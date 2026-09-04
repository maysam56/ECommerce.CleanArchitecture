using ECommerece.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Interfaces.IRepository.ProductRepository
{
    public interface  IProductReadRepository 
    {
        Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<List<Product>> GetAllAsync( CancellationToken cancellationToken);
        Task<bool> ExistsBySkuAsync(string sku , CancellationToken cancellationToken);
    }
}
