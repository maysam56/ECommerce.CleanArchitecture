using ECommerece.Domain.Entities;


namespace ECommerce.Application.Interfaces.IRepository.CustomerRepository
{
    public  interface ICustomerWriteRepository
    {
        Task AddAsync(Customer customer, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);

    }
}
