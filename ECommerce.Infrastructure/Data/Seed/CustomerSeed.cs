using ECommerece.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data.Seed
{
    public static class CustomerSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    FullName = "Sarah Connor",
                    Email = "sarah.connor@sky.net",
                    IsVip = true
                },

                new Customer
                {
                    Id = 2,
                    FullName = "John Doe",
                    Email = "john.doe@example.com",
                    IsVip = false
                },

                new Customer
                {
                    Id = 3,
                    FullName = "Jane Smith",
                    Email = "jane.smith@example.com",
                    IsVip = false
                }
            );
        }
    }
}