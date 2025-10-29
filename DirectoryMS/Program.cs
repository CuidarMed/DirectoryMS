using Application.Interfaces;
using Application.Services;
using Infraestructure.Command;
using Infraestructure.Persistence;
using Infraestructure.Queries;
using Infrastructure.Command;
using Infrastructure.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Obtenego la cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    // Si esta parte falla, es la causa del error.
    throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no fue encontrada en appsettings.json.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString) // Pasa la cadena LEÍDA aquí
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
    for(var attempt = 1; attempt <= maxRetries; attempt++)
    {
        try
        {
            logger.LogInformation("Applying migrations... Attempt {Attempt} of {MaxRetries}", attempt, maxRetries);
            dbContext.Database.Migrate();
            logger.LogInformation("Migrations applied successfully.");
            break;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "An error occurred while applying migrations on attempt {Attempt} of {MaxRetries}", attempt, maxRetries);
            if(attempt == maxRetries)
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

app.UseAuthorization();

app.MapControllers();

app.Run();
