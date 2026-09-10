using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace ECommerce.Infrastructure.BackgroundJobs;

public class CartReminderBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public CartReminderBackgroundService(IServiceScopeFactory scopeFactory)  
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider
                .GetRequiredService<IMediator>();

            await mediator.Send(new SendCartReminderEmailsCommand(), stoppingToken);
              
            await Task.Delay(TimeSpan.FromHours(1),stoppingToken);
               
                
        }
    }
}