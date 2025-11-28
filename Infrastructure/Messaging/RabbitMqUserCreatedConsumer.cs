using Application.Contracts.Events;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Messaging;

public class RabbitMqUserCreatedConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _config;
    private readonly ILogger<RabbitMqUserCreatedConsumer> _logger;
    private IConnection _connection;
    private IChannel _channel;
    private const string QueueName = "user-created";

    public RabbitMqUserCreatedConsumer(
        IServiceProvider serviceProvider,
        IConfiguration config,
        ILogger<RabbitMqUserCreatedConsumer> logger)
    {
        _serviceProvider = serviceProvider;
        _config = config;
        _logger = logger;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            var factory = new ConnectionFactory
            {
                HostName = _config["RabbitMQ:Host"] ?? "rabbitmq",
                UserName = _config["RabbitMQ:User"] ?? "user",
                Password = _config["RabbitMQ:Pass"] ?? "pass",
                AutomaticRecoveryEnabled = true,
                TopologyRecoveryEnabled = true,
                RequestedHeartbeat = TimeSpan.FromSeconds(60),
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
                // Importante: configurar timeouts
                ContinuationTimeout = TimeSpan.FromSeconds(30),
                HandshakeContinuationTimeout = TimeSpan.FromSeconds(30)
            };

            _connection = await factory.CreateConnectionAsync(cancellationToken: cancellationToken);

            // Eventos de monitoreo de la conexión
            _connection.ConnectionShutdownAsync += async (sender, args) =>
            {
                _logger.LogWarning("Conexión RabbitMQ cerrada: {Reason}", args.ReplyText);
                await Task.CompletedTask;
            };

            _connection.CallbackExceptionAsync += async (sender, args) =>
            {
                _logger.LogError(args.Exception, "Error en callback de RabbitMQ");
                await Task.CompletedTask;
            };

            _channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

            // Configurar QoS para procesar mensajes de uno en uno
            await _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false, cancellationToken);

            await _channel.QueueDeclarePassiveAsync(QueueName, cancellationToken);

            _logger.LogInformation("DirectoryMS: Queue '{QueueName}' encontrada correctamente", QueueName);

            await base.StartAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al iniciar el consumer de RabbitMQ");
            throw;
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (ch, ea) =>
        {
            try
            {
                _logger.LogDebug("Mensaje recibido de cola '{QueueName}', DeliveryTag: {DeliveryTag}",
                    QueueName, ea.DeliveryTag);

                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var evt = JsonSerializer.Deserialize<UserCreatedEvent>(json);

                if (evt == null)
                {
                    _logger.LogWarning("No se pudo deserializar el evento. JSON: {Json}", json);
                    await _channel.BasicNackAsync(ea.DeliveryTag, false, false, stoppingToken);
                    return;
                }

                using var scope = _serviceProvider.CreateScope();
                var handler = scope.ServiceProvider.GetRequiredService<IUserCreatedEventHandler>();

                await handler.HandleAsync(evt, stoppingToken);

                // ACK solo después de procesar exitosamente
                await _channel.BasicAckAsync(ea.DeliveryTag, false, stoppingToken);

                _logger.LogInformation("Evento UserCreated procesado exitosamente para usuario: {UserId}", evt.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error procesando mensaje de RabbitMQ. DeliveryTag: {DeliveryTag}. JSON: {Json}",
                    ea.DeliveryTag, Encoding.UTF8.GetString(ea.Body.ToArray()));

                try
                {
                    // NO reencolar si es error de datos (InvalidOperationException, ArgumentException, etc.)
                    bool requeue = ex is not InvalidOperationException
                                   and not ArgumentNullException
                                   and not ArgumentException
                                   and not JsonException;

                    if (!requeue)
                    {
                        _logger.LogWarning("Mensaje descartado (no se reencolará) debido a error de datos");
                    }

                    await _channel.BasicNackAsync(ea.DeliveryTag, false, requeue, stoppingToken);
                }
                catch (Exception nackEx)
                {
                    _logger.LogError(nackEx, "Error al hacer NACK del mensaje");
                }
            }
        };

        consumer.ShutdownAsync += async (sender, args) =>
        {
            _logger.LogWarning("Consumer cerrado: {Reason}", args.ReplyText);
            await Task.CompletedTask;
        };

        // CRÍTICO: Await el resultado de BasicConsumeAsync
        string consumerTag = await _channel.BasicConsumeAsync(
            queue: QueueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken);

        _logger.LogInformation("DirectoryMS: consumidor suscripto a '{QueueName}' con tag '{ConsumerTag}'",
            QueueName, consumerTag);

        // Mantener el servicio ejecutándose hasta que se cancele
        try
        {
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (TaskCanceledException)
        {
            _logger.LogInformation("Consumer detenido por cancelación");
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deteniendo consumer de RabbitMQ...");

        try
        {
            if (_channel != null)
            {
                await _channel.CloseAsync(cancellationToken);
                await _channel.DisposeAsync();
            }

            if (_connection != null)
            {
                await _connection.CloseAsync(cancellationToken);
                await _connection.DisposeAsync();
            }

            _logger.LogInformation("Consumer de RabbitMQ detenido correctamente");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al detener el consumer de RabbitMQ");
        }

        await base.StopAsync(cancellationToken);
    }
}