using ECommerece.Domain.Entities;

namespace ECommerce.Application.Interfaces.IRepository.OrderRepository
{
    public interface IOrderWriteRepository
    {
        Task AddAsync(Order order, CancellationToken cancellationToken);
    }
}
