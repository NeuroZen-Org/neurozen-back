﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using neurozen.API.Sales.Domain.Entities;

namespace neurozen.API.Sales.Infrastructure.Persistence.EFC.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Status).HasMaxLength(50).HasDefaultValue("pending");
        builder.Property(o => o.Total).HasColumnType("numeric(12,2)").HasDefaultValue(0m);
        builder.Property(o => o.Currency).HasMaxLength(10).HasDefaultValue("USD");
        builder.Property(o => o.PaymentInfo).HasColumnType("json");
        builder.Property(o => o.Metadata).HasColumnType("json");
        builder.Property(o => o.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(o => o.UpdatedAt).HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
        builder.HasOne(o => o.User).WithMany(u => u.Orders).HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(o => o.ShippingAddress).WithMany().HasForeignKey(o => o.ShippingAddressId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(o => o.BillingAddress).WithMany().HasForeignKey(o => o.BillingAddressId).OnDelete(DeleteBehavior.NoAction);
    }
}
