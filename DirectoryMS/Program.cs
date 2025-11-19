using Application.Interfaces;
using Application.Services;
using DirectoryMS.Converters;
using Infraestructure.Command;
using Infraestructure.Persistence;
using Infraestructure.Queries;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Features; // Necesario para configurar FormOptions


// BUILDER CONFIGURATION

var builder = WebApplication.CreateBuilder(args);

// -------------------- 1. Core Services & Media Limits --------------------

// Aumentar el límite de tamaño del request body para Kestrel (para imágenes/archivos grandes)
builder.WebHost.ConfigureKestrel(options =>
{
    // Límite de 50MB
    options.Limits.MaxRequestBodySize = 50 * 1024 * 1024;
});

// Configuración de Request Body y Controladores (MVC)
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // JSON Converters para DateOnly y Enums
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        options.JsonSerializerOptions.Converters.Add(new NullableDateOnlyJsonConverter());
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        // Política de nombramiento estándar
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        // Suprimir el filtro de validación predeterminado si se usan filtros personalizados o FluentValidation
        options.SuppressModelStateInvalidFilter = false;
    });

// Configuración para permitir tamaños de formulario grandes (necesario junto con Kestrel)
builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = int.MaxValue;
    options.MemoryBufferThreshold = int.MaxValue;
});

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// -------------------- 2. Database & Localization Configuration --------------------

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection'.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, sqlServerOptions =>
    {
        // Estrategia de reintento robusta para entornos inestables/Docker
        sqlServerOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    })
);

// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configuración de Localización (para garantizar el formato de fecha/número consistente)
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var culture = new CultureInfo("es-US");
    options.DefaultRequestCulture = new RequestCulture(culture);
    options.SupportedCultures = new[] { culture };
    options.SupportedUICultures = new[] { culture };
});

// -------------------- 3. Dependency Injection (Application/Infrastructure) --------------------

// ========== QUERIES (Lectura) - Infrastructure ==========
builder.Services.AddScoped<IDoctorQuery, DoctorQuery>();
builder.Services.AddScoped<IPatientQuery, PatientQuery>();

// ========== COMMANDS (Escritura) - Infrastructure ==========
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


// APP CONFIGURATION

var app = builder.Build();

app.UseRequestLocalization();

// ========== Aplicar Migraciones con Reintento (Retries) ==========
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // Se inyecta ILogger<Program> ya que no hay una clase Program formal
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    const int maxRetries = 10;
    for (var attempt = 1; attempt <= maxRetries; attempt++)
    {
        try
        {
            logger.LogInformation("Applying migrations... Attempt {Attempt} of {MaxRetries}", attempt, maxRetries);
            // La migración real ocurre aquí
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
            // Espera antes de reintentar para dar tiempo a que la DB se inicialice
            await Task.Delay(TimeSpan.FromSeconds(3));
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

// Usar la política CORS definida
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();