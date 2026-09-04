using ECommerce.DAL.Entities;
using ECommerece.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data.Seed
{
    public static class OrderSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    CustomerId = 1,
                    CreatedAt = new DateTime(
                        2026, 1, 15, 10, 30, 0,
                        DateTimeKind.Utc),
                    Status = OrderStatus.Paid,
                    Subtotal = 120.00m,
                    DiscountAmount = 18.00m,
                    TaxAmount = 14.28m,
                    ShippingFee = 75.00m,
                    TotalAmount = 191.28m
                },

                new Order
                {
                    Id = 2,
                    CustomerId = 2,
                    CreatedAt = new DateTime(
                        2026, 2, 1, 14, 0, 0,
                        DateTimeKind.Utc),
                    Status = OrderStatus.Pending,
                    Subtotal = 45.50m,
                    DiscountAmount = 0.00m,
                    TaxAmount = 6.37m,
                    ShippingFee = 75.00m,
                    TotalAmount = 126.87m
                }
            );
        }
    }
}