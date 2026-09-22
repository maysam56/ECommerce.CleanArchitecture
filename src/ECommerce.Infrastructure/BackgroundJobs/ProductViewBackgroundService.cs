using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Application.Interfaces.IRepository.ProductRepository;
using ECommerce.Application.Interfaces.IServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ECommerce.Infrastructure.BackgroundJobs;

public class ProductViewBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public ProductViewBackgroundService(
        IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();

            var productViewCounter =
                scope.ServiceProvider
                    .GetRequiredService<IProductViewCounter>();

            var productReadRepository =
                scope.ServiceProvider
                    .GetRequiredService<IProductReadRepository>();

            var productWriteRepository =
                scope.ServiceProvider
                    .GetRequiredService<IProductWriteRepository>();

            var unitOfWork =
                scope.ServiceProvider
                    .GetRequiredService<IUnitOfWork>();

            var productIds =
                await productViewCounter.GetViewedProductIdsAsync(stoppingToken);
                    

            foreach (var productId in productIds)
            {
                var count = await productViewCounter.TakeCountAsync(productId,stoppingToken);

                var product = await productReadRepository.GetByIdAsync(productId,stoppingToken);

                if (product == null) continue;
                   
                product.IncreaseViewCount((int)count);

                await productWriteRepository.UpdateProductAsync(product);

                await unitOfWork.SaveChangesAsync();

                await productViewCounter.RemoveViewedProductAsync(productId,stoppingToken);                    
                                      
            }

            await Task.Delay(TimeSpan.FromMinutes(1),stoppingToken);
              
                
        }
    }
}