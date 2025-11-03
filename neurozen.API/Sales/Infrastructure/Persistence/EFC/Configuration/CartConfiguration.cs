﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using neurozen.API.Sales.Domain.Entities;

namespace neurozen.API.Sales.Infrastructure.Persistence.EFC.Configuration;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("carts");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.SessionId).HasMaxLength(255);
        builder.Property(c => c.Metadata).HasColumnType("json");
        builder.Property(c => c.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(c => c.UpdatedAt).HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
        builder.HasOne(c => c.User).WithMany(u => u.Carts).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.SetNull);
    }
}
