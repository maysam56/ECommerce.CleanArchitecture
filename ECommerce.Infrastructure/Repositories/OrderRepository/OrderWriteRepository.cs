using ECommerece.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ECommerce.Infrastructure.Data.Context;
using ECommerce.Application.Interfaces.IRepository.OrderRepository;
namespace ECommerce.Infrastructure.Repositories.OrderRepository
{
    public class OrderWriteRepository : IOrderWriteRepository
    {
        private readonly AppWriteDbContext _context;

        public OrderWriteRepository(AppWriteDbContext context)
        {
            _context = context;
        }
        // Add Order Async
        public async Task AddAsync(Order order , CancellationToken cancellationToken)
        {
            await _context.Orders.AddAsync(order, cancellationToken);
           
        }
        // Save Changes Async
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
