using ECommerece.Domain.Entities;

namespace ECommerce.Application.Interfaces.IRepository.CouponRepository
{
    public interface  ICouponReadRepository
    {
        Task<Coupon?> GetByCodeAsync(string code, CancellationToken cancellationToken);
    }
}
