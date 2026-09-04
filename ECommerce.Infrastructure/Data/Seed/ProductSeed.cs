using ECommerce.Infrastructure.Data.Context;
using ECommerece.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data.Seed
{
    public static class ProductSeed
    {
        public static async Task SeedAsync(AppReadDbContext context)
        {
            if (await context.Products.AnyAsync())
                return;

            var products = new List<Product>();

            var product1 = new Product
            {
                Id = 1,
                Name = "Mechanical Keyboard",
                SKU = "TECH-MK-01"
            };

            product1.SetPrice(120.00m);
            product1.SetStockQuantity(25);

            products.Add(product1);


            var product2 = new Product
            {
                Id = 2,
                Name = "Wireless Ergonomic Mouse",
                SKU = "TECH-WM-02"
            };

            product2.SetPrice(45.50m);
            product2.SetStockQuantity(40);

            products.Add(product2);


            var product3 = new Product
            {
                Id = 3,
                Name = "UltraWide Monitor 34\"",
                SKU = "DISP-UW-03"
            };

            product3.SetPrice(650.00m);
            product3.SetStockQuantity(8);

            products.Add(product3);


            var product4 = new Product
            {
                Id = 4,
                Name = "USB-C Multiport Dock",
                SKU = "ACC-DK-04"
            };

            product4.SetPrice(85.00m);
            product4.SetStockQuantity(15);

            products.Add(product4);


            var product5 = new Product
            {
                Id = 5,
                Name = "Noise Cancelling Headphones",
                SKU = "AUD-NC-05"
            };

            product5.SetPrice(220.00m);
            product5.SetStockQuantity(12);

            products.Add(product5);


            await context.Products.AddRangeAsync(products);

            await context.SaveChangesAsync();
        }
    }
}