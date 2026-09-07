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
}