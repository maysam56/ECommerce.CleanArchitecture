using ECommerce.Application.Interfaces.IRepository.CartRepository;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories.CartRepository;

public class CartReadRepository : ICartReadRepository
{
    private readonly AppReadDbContext _context;

    public CartReadRepository(AppReadDbContext context)
    {
        _context = context;
    }

    public async Task<List<CartItem>> GetExpiredItemsAsync(
        DateTime expirationDate,
        CancellationToken cancellationToken)
    {
        return await _context.CartItems
            .Include(x => x.Cart)
            .Where(x => x.AddedAt <= expirationDate)
            .ToListAsync(cancellationToken);
    }
    public async Task<List<CartItem>> GetItemsForReminderAsync(
         CancellationToken cancellationToken)
    {
        var fourDaysAgo = DateTime.UtcNow.AddDays(-4);

        return await _context.CartItems
            .Include(item => item.Cart)
                .ThenInclude(cart => cart.customer)
            .Include(item => item.Product)
            .Where(item =>
                item.AddedAt <= fourDaysAgo &&
                item.ReminderSentAt == null)
            .ToListAsync(cancellationToken);
    }
}