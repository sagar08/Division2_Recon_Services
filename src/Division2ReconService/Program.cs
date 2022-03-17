using Division2ReconService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

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

// Add services to the container.

builder.Services.AddControllers();

// Add HealthChecks
builder.Services.AddHealthChecks()
    .AddCheck("Service", () => { return HealthCheckResult.Healthy("OK"); })
    .AddCheck("SQL Database", () => { return HealthCheckResult.Healthy("OK"); });
//builder.Services.AddHealthChecksUI();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed Data
using (var scope = app.Services.CreateScope())
{
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

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//    endpoints.MapHealthChecks("/health");
//});

app.Run();
