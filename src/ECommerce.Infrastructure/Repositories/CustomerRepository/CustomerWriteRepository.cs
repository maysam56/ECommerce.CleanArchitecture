using ECommerce.Application.Interfaces.IRepository.CustomerRepository;
using ECommerce.Infrastructure.Data.Context;
using ECommerece.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Repositories.CustomerRepository
{
    public class CustomerWriteRepository : ICustomerWriteRepository
    {
        private readonly AppWriteDbContext _context;

        public CustomerWriteRepository(AppWriteDbContext context) {
            _context = context;
        }
   
     
          public async Task AddAsync(Customer customer , CancellationToken cancellationToken)
          {
            await _context.Customers.AddAsync(customer, cancellationToken);
          }
         public async Task SaveChangesAsync(CancellationToken cancellationToken)
         {
            await _context.SaveChangesAsync(cancellationToken);
         }
    }
}
