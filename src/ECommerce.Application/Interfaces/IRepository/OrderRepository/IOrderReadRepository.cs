using ECommerece.Domain.Entities;

namespace ECommerce.Application.Interfaces.IRepository.OrderRepository
{
    public interface IOrderReadRepository 
    {
        Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyList<Order>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken);
        Task<Order?> GetForCancelAsync(int id , CancellationToken cancellationToken);
        Task<Order?> GetOrderForInvoiceAsync(int orderId,CancellationToken cancellationToken);
        Task<IReadOnlyList<Order>> GetOrdersForInvoiceAsync(CancellationToken cancellationToken);
    }
}
 