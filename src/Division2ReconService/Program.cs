using Division2ReconService.Data;
using Division2ReconService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// Configure Logger
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Configure Database
builder.Services.AddDbContext<Division2ReconDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteDivision2ReconDB"));
});

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Configure JSON
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Configure Logger
builder.Services.ConfigureLoggerService();

// Add services to the container.

builder.Services.AddControllers();

// Add HealthChecks
builder.Services.AddHealthChecks()
    .AddCheck("Service", () => { return HealthCheckResult.Healthy("OK"); })
    .AddCheck("SQL Database", () => { return HealthCheckResult.Healthy("OK"); });
//builder.Services.AddHealthChecksUI();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger
builder.Services.ConfigureSwagger();

var app = builder.Build();

// Configure Global Exception handler
app.ConfigureExceptionHandler();

using (var scope = app.Services.CreateScope())
{
    // Seed Data
    var dbContext = scope.ServiceProvider.GetRequiredService<Division2ReconDbContext>();
    var seedData = new SeedData(dbContext);
    seedData.SeedInitialDataAsync().Wait();
}

// Configure CORS
app.UseCors(builder => builder
.AllowAnyHeader()
.AllowAnyMethod()
.AllowAnyOrigin());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Division2 Recon API V1");
});

app.MapHealthChecks("/health");

app.Run();
