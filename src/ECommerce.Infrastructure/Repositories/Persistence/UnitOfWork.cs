using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Infrastructure.Data.Context;

namespace ECommerce.Infrastructure.Repositories.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppWriteDbContext _context;

    public UnitOfWork(AppWriteDbContext context )
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}