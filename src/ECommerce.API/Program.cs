using ECommerce.Application.Common;
using ECommerce.Application.Common.Behavior;
using ECommerce.Infrastructure.Extensions;
using FluentValidation;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

 DependencyInjection.AddInfrastructure(
    builder.Services,
    builder.Configuration
);

builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
   

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly( typeof(ApplicationAssemblyMarker).Assembly));
       

builder.Services.AddValidatorsFromAssembly(
    typeof(ApplicationAssemblyMarker).Assembly);

//builder.Services.AddInfrastructure(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
