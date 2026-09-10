using ECommerce.Application.Interfaces.IRepository.CartRepository;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data.Context;

namespace ECommerce.Infrastructure.Repositories.CartRepository;

public class CartWriteRepository : ICartWriteRepository
{
    private readonly AppWriteDbContext _context;

    public CartWriteRepository(AppWriteDbContext context)
    {
        _context = context;
    }

    public void MarkReminderAsSent(CartItem item)
    {
        item.ReminderSentAt = DateTime.UtcNow;

        _context.CartItems.Update(item);
    }
}