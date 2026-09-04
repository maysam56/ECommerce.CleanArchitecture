using ECommerce.Application.Interfaces.IRepository.OrderRepository;
using ECommerce.Infrastructure.Data.Context;
using ECommerece.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Repositories.OrderRepository
{
    public class OrderReadRepository :IOrderReadRepository
    {
        private readonly AppReadDbContext _context;

        public OrderReadRepository(AppReadDbContext context)
        {
            _context = context;
        }

        // Get Order By Id Async
        public async Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Orders
              .Include(o => o.Items)
              .ThenInclude(i => i.Product)
              .Include(o => o.Payment)
              .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

        }

        // Get By Customer Id Async
        public async Task<IReadOnlyList<Order>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken)
        {
            return await _context.Orders
           .Include(o => o.Items)
           .Where(o => o.CustomerId == customerId)
           .ToListAsync(cancellationToken);
        }

        // Get Order For Cancel Async
        public async Task<Order?> GetForCancelAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

        }

    }
}
