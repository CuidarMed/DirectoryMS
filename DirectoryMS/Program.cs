using Application.Interfaces;
using Application.Services;
using Infraestructure.Command;
using Infraestructure.Persistence;
using Infraestructure.Queries;
using Infrastructure.Command;
using Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using DirectoryMS.Converters;
using FluentValidation;
using System.Reflection;
using DirectoryMS.Models;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configurar el serializador para manejar DateOnly correctamente
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        options.JsonSerializerOptions.Converters.Add(new NullableDateOnlyJsonConverter());
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        // Permitir nombres de propiedades en camelCase
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        // Devolver ProblemDetails estándar en validaciones
        options.InvalidModelStateResponseFactory = context =>
            new Microsoft.AspNetCore.Mvc.BadRequestObjectResult(new Microsoft.AspNetCore.Mvc.ValidationProblemDetails(context.ModelState));
    });

// Aumentar el límite de tamaño del request body
builder.Services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(options =>
{
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = int.MaxValue;
    options.MemoryBufferThreshold = int.MaxValue;
});

// Aumentar el límite del request body en Kestrel
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 50 * 1024 * 1024; // 50MB
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ========== CONFIGURACIÓN GATEWAY ==========
builder.Services.Configure<GatewaySettings>(options =>
{
    var gatewayUrl = Environment.GetEnvironmentVariable("AUTH_GATEWAY_URL")
        ?? builder.Configuration["Gateway:AuthServiceUrl"]
        ?? "http://gatewayms_api:8080/auth";

    options.AuthServiceUrl = gatewayUrl;
});

// ========== HttpClient para AuthTokenService (obtener tokens) ==========
builder.Services.AddHttpClient<IAuthTokenService, AuthTokenService>((serviceProvider, client) =>
{
    var gatewaySettings1 = serviceProvider.GetRequiredService<IOptions<GatewaySettings>>().Value;
    client.BaseAddress = new Uri(gatewaySettings1.AuthServiceUrl);
    client.Timeout = TimeSpan.FromSeconds(30);
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
})
.SetHandlerLifetime(TimeSpan.FromMinutes(5));

// ========== HttpClient para AuthGatewayService (llamadas autenticadas) ==========
builder.Services.AddHttpClient<IAuthGatewayService, AuthGatewayService>((serviceProvider, client) =>
{
    var gatewaySettings = serviceProvider.GetRequiredService<IOptions<GatewaySettings>>().Value;
    client.BaseAddress = new Uri(gatewaySettings.AuthServiceUrl);
    client.Timeout = TimeSpan.FromSeconds(30);
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
})
.SetHandlerLifetime(TimeSpan.FromMinutes(5));

// Obtenego la cadena de conexion
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, sqlServerOptions =>
    {
        sqlServerOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    })
    .ConfigureWarnings(warnings =>
        warnings.Ignore(RelationalEventId.PendingModelChangesWarning))
);

// ========== QUERIES (lectura) - Infrastructure ==========
builder.Services.AddScoped<IDoctorQuery, DoctorQuery>();
builder.Services.AddScoped<IPatientQuery, PatientQuery>();

// ========== COMMANDS (escritura) - Infrastructure ==========
builder.Services.AddScoped<IDoctorCommand, DoctorCommand>();
builder.Services.AddScoped<IPatientCommand, PatientCommand>();

// ========== SERVICES - Doctors ==========
builder.Services.AddScoped<ICreateDoctorService, CreateDoctorService>();
builder.Services.AddScoped<ISearchDoctorService, SearchDoctorService>();
builder.Services.AddScoped<IUpdateDoctorService, UpdateDoctorService>();

// ========== SERVICES - Patients ==========
builder.Services.AddScoped<ICreatePatientService, CreatePatientService>();
builder.Services.AddScoped<ISearchPatientService, SearchPatientService>();
builder.Services.AddScoped<IUpdatePatientService, UpdatePatientService>();

// ========== FluentValidation ==========
builder.Services.AddValidatorsFromAssembly(Assembly.Load("Application"));


builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("es-US");
    options.SupportedCultures = new[] { new CultureInfo("es-US") };
    options.SupportedUICultures = new[] { new CultureInfo("es-US") };
});


var app = builder.Build();
app.UseRequestLocalization();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    const int maxRetries = 10;
    for (var attempt = 1; attempt <= maxRetries; attempt++)
    {
        try
        {
            logger.LogInformation("Applying migrations... Attempt {Attempt} of {MaxRetries}", attempt, maxRetries);
            dbContext.Database.Migrate();
            logger.LogInformation("Migrations applied successfully.");
            break;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while applying migrations on attempt {Attempt} of {MaxRetries}", attempt, maxRetries);
            if (attempt == maxRetries)
            {
                logger.LogCritical("Max migration attempts reached. Exiting application.");
                throw;
            }
            await Task.Delay(TimeSpan.FromSeconds(3)); // Wait before retrying
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();