using ECommerece.Domain.Entities;


namespace ECommerce.Application.Interfaces.IRepository.CustomerRepository
{
    public interface ICustomerReadRepository 
    {
        Task<Customer?> GetByIdAsync(int id, CancellationToken cancellationToken);
     
        Task<bool> checkEmailExsist(string email, CancellationToken cancellationToken);
    }
}
