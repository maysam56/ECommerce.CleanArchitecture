using ECommerce.Application.Interfaces.IRepository.ProductRepository;
using ECommerce.Infrastructure.Data.Context;
using ECommerece.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace ECommerce.Infrastructure.Repositories.ProductRepository
{
    public class ProductReadRepository : IProductReadRepository
    {
        private readonly AppReadDbContext _context;

        public ProductReadRepository(AppReadDbContext context) {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(int id , CancellationToken cancellationToken)
        {
          return await _context.Products.FindAsync(id, cancellationToken);
        }

        public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Products.ToListAsync(cancellationToken);
        }
 

        public async Task<bool> ExistsBySkuAsync(string sku , CancellationToken cancellationToken)
        {

            return await _context.Products.AnyAsync(p => p.SKU.ToLower() == sku.ToLower(), cancellationToken);
        }

    }

}
