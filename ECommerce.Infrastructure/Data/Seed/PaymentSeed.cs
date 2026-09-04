using ECommerece.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data.Seed
{
    public static class PaymentSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>().HasData(
                new Payment
                {
                    Id = 1,
                    OrderId = 1,
                    Amount = 191.28m,
                    PaymentDate = new DateTime(
                        2026, 1, 15, 10, 35, 0,
                        DateTimeKind.Utc),
                    TransactionReference = "TX-MOCK-10001",
                    IsSuccess = true
                }
            );
        }
    }
}