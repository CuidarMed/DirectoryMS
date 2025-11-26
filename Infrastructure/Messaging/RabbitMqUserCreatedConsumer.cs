using Application.Contracts.Events;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace Infrastructure.Messaging;

public class RabbitMqUserCreatedConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _config;

    private IConnection _connection;
    private IChannel _channel;

    private const string QueueName = "user-created";

    public RabbitMqUserCreatedConsumer(
        IServiceProvider serviceProvider,
        IConfiguration config)
    {
        _serviceProvider = serviceProvider;
        _config = config;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _config["RabbitMQ:Host"] ?? "rabbitmq",
            UserName = _config["RabbitMQ:User"] ?? "user",
            Password = _config["RabbitMQ:Pass"] ?? "pass"
        };

        // 7.2.0: conexión async
        _connection = await factory.CreateConnectionAsync(cancellationToken: cancellationToken);

        _channel = await _connection.CreateChannelAsync();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (ch, ea) =>
        {
            var json = Encoding.UTF8.GetString(ea.Body.ToArray());
            var evt = JsonSerializer.Deserialize<UserCreatedEvent>(json);

            using var scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<IUserCreatedEventHandler>();

            await handler.HandleAsync(evt, stoppingToken);

            await _channel.BasicAckAsync(ea.DeliveryTag, false, stoppingToken);

            await _channel.QueueDeclarePassiveAsync(QueueName, stoppingToken);

            Console.WriteLine("DirectoryMS: Queue 'user-created' encontrada correctamente.");

            await base.StartAsync(stoppingToken);
        };
        // suscribirse a la cola: contentReference[oaicite:7]{index=7}
        _= _channel.BasicConsumeAsync(
            queue: QueueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken);

        return Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
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

        await base.StopAsync(cancellationToken);
    }
}
