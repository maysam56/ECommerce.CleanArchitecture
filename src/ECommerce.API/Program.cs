using ECommerce.Application.Common;
using ECommerce.Application.Common.Behavior;
using ECommerce.Infrastructure.Extensions;
using FluentValidation;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Infrastructure
DependencyInjection.AddInfrastructure(
    builder.Services,
    builder.Configuration
);

//Memory Cache
builder.Services.AddMemoryCache();

// Validation Behavior
builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>)
);

// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(
        typeof(ApplicationAssemblyMarker).Assembly));

// FluentValidation
builder.Services.AddValidatorsFromAssembly(
    typeof(ApplicationAssemblyMarker).Assembly);

var app = builder.Build();

// Swagger Middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();