using ECommerce.Application.Interfaces.IRepository.CustomerRepository;
using ECommerce.Infrastructure.Data.Context;
using ECommerece.Domain.Entities;

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
    
    }
}
