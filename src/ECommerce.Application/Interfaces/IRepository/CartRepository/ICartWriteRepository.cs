

using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.IRepository.CartRepository
{
     public interface ICartWriteRepository
     {
        void MarkReminderAsSent(CartItem item);
    }
}
