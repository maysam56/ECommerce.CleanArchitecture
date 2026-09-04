using ECommerce.Application.Interfaces.IRepository.CustomerRepository;
using ECommerce.Infrastructure.Data.Context;
using ECommerece.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Repositories.CustomerRepository
{
    public class CustomerReadRepository :ICustomerReadRepository
    {
        private readonly AppReadDbContext _context;

        public CustomerReadRepository(AppReadDbContext context)
        {
            _context = context;
        }
        public async Task<Customer?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {

            return await _context.Customers
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        }
        public async Task<bool> checkEmailExsist(string email, CancellationToken cancellationToken)
        {
            return await _context.Customers.AnyAsync(c => c.Email.ToLower() == email.ToLower(), cancellationToken);

        }
    }
}
