using ECommerce.Application.Interfaces.IRepository.ProductRepository;
using ECommerce.Infrastructure.Data.Context;
using ECommerece.Domain.Entities;


namespace ECommerce.Infrastructure.Repositories.ProductRepository
{
    public class ProductWriteRepository :IProductWriteRepository
    {
        private readonly AppWriteDbContext _context;

        public ProductWriteRepository(AppWriteDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Product product, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(product, cancellationToken);
        }
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
        }
        public async Task AddProductAsync(Product product, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(product, cancellationToken);

        }
        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
        }
    }
}
