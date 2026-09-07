using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ECommerce.Infrastructure.BackgroundJobs;

public class CartCleanupBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<CartCleanupBackgroundService> _logger;

    public CartCleanupBackgroundService(
        IServiceScopeFactory scopeFactory,
        ILogger<CartCleanupBackgroundService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();

                var mediator = scope.ServiceProvider
                    .GetRequiredService<IMediator>();

                await mediator.Send(
                    new RemoveExpiredCartItemsCommand(),
                    stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error while cleaning expired cart items.");
            }

            await Task.Delay(
                TimeSpan.FromHours(1),
                stoppingToken);
        }
    }
}