using ECommerece.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Interfaces.IRepository.ProductRepository
{
    public  interface IProductWriteRepository
    {
        Task DeleteProductAsync(Product product);
        Task AddProductAsync(Product product, CancellationToken cancellationToken);
        Task UpdateProductAsync(Product product);
        
       
    }
}
