using ECommerce.Application.Interfaces.IRepository.CouponRepository;
using ECommerce.Infrastructure.Data.Context;
using ECommerece.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories.CouponRepository
{
    public class CouponReadRepository : ICouponReadRepository
    {
        private readonly AppReadDbContext _context; 
        public CouponReadRepository(AppReadDbContext context) {
            
            _context = context; 
        }
        public async Task<Coupon?> GetByCodeAsync(string code, CancellationToken cancellationToken) {

            return await _context.Coupons.FirstOrDefaultAsync(c => c.Code.ToUpper() == code.ToUpper(), cancellationToken); 
        }
    }
}
