using ECommerece.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data.Seed
{
    public static class CouponSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon
                {
                    Id = 1,
                    Code = "WELCOME10",
                    DiscountPercentage = 10.00m,
                    IsActive = true
                },
                new Coupon
                {
                    Id = 2,
                    Code = "SUMMER20",
                    DiscountPercentage = 20.00m,
                    IsActive = true
                },
                new Coupon
                {
                    Id = 3,
                    Code = "EXPIRED50",
                    DiscountPercentage = 50.00m,
                    IsActive = false
                }
            );
        }
    }
}