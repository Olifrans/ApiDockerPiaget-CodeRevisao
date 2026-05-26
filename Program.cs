using Microsoft.EntityFrameworkCore;
using ApiDockerPiaget.Data;
using System.Text.Json.Serialization;
using FluentValidation;
using ApiDockerPiaget.Validators;
using AutoMapper;
using ApiDockerPiaget.Mappings;
using ApiDockerPiaget.Middleware;
using ApiDockerPiaget.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;   // ? Adicionar

var builder = WebApplication.CreateBuilder(args);

// ====================== SERVICES ======================
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddValidatorsFromAssemblyContaining<AlunoValidator>();

builder.Services.AddCors(options => options.AddPolicy("AllowAll", p =>
    p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Escola Piaget",
        Version = "v1"
    });
});

// Health Checks Personalizados
builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>("database", HealthStatus.Unhealthy)
    .AddDbContextCheck<AppDbContext>("ef-core");



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// ====================== MIDDLEWARE ======================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.RoutePrefix = string.Empty);
    app.UseCors("AllowAll");
}

app.UseMiddleware<GlobalExceptionMiddleware>();

// Health Checks Endpoints
app.MapHealthChecks("/health");
app.MapHealthChecks("/health/ready", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();