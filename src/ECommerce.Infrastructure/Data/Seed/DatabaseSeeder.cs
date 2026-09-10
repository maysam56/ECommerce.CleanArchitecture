using ECommerce.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data.Seed;

internal static class DatabaseSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        CouponSeed.Seed(modelBuilder);
        CustomerSeed.Seed(modelBuilder);
        OrderItemSeed.Seed(modelBuilder);
        OrderSeed.Seed(modelBuilder);
        PaymentSeed.Seed(modelBuilder);
    }

    public static async Task SeedAsync(AppWriteDbContext context)
    {
        await ProductSeed.SeedAsync(context);
    }
}