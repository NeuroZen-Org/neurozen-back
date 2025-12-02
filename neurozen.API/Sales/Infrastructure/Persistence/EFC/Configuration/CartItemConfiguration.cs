﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using neurozen.API.Sales.Domain.Entities;

namespace neurozen.API.Sales.Infrastructure.Persistence.EFC.Configuration;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("cart_items");
        builder.HasKey(ci => ci.Id);
        builder.Property(ci => ci.Quantity).IsRequired().HasDefaultValue(1);
        builder.Property(ci => ci.UnitPrice).HasColumnType("numeric(10,2)").IsRequired();
        builder.Property(ci => ci.Metadata).HasColumnType("json");
        builder.HasOne(ci => ci.Cart).WithMany(c => c.Items).HasForeignKey(ci => ci.CartId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(ci => ci.Product).WithMany().HasForeignKey(ci => ci.ProductId).OnDelete(DeleteBehavior.NoAction);
    }
}
