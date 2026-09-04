using ECommerece.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Subtotal)
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.DiscountAmount)
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.TaxAmount)
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.ShippingFee)
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

  
        }

    }
}
