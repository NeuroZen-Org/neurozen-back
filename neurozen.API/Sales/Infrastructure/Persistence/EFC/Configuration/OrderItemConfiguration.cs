﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using neurozen.API.Sales.Domain.Entities;

namespace neurozen.API.Sales.Infrastructure.Persistence.EFC.Configuration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("order_items");
        builder.HasKey(oi => oi.Id);
        builder.Property(oi => oi.Name).HasMaxLength(255);
        builder.Property(oi => oi.Sku).HasMaxLength(100);
        builder.Property(oi => oi.Quantity).HasDefaultValue(1);
        builder.Property(oi => oi.UnitPrice).HasColumnType("numeric(10,2)").IsRequired();
        builder.Property(oi => oi.Metadata).HasColumnType("json");
        builder.HasOne(oi => oi.Order).WithMany(o => o.Items).HasForeignKey(oi => oi.OrderId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<Order>().WithMany().HasForeignKey(oi => oi.ProductId).OnDelete(DeleteBehavior.NoAction);
    }
}
