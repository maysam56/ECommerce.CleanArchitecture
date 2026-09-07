using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.IRepository.CartRepository
{
    public interface ICartReadRepository
    {
        Task<List<CartItem>> GetExpiredItemsAsync(
        DateTime expirationDate,
        CancellationToken cancellationToken);
    }
}
