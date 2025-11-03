﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using neurozen.API.Catalog.Domain.Entities;

namespace neurozen.API.Catalog.Infrastructure.Persistence.EFC.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Sku).HasMaxLength(100);
        builder.HasIndex(p => p.Sku).IsUnique();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
        builder.Property(p => p.Slug).IsRequired().HasMaxLength(255);
        builder.HasIndex(p => p.Slug).IsUnique();
        builder.Property(p => p.Description).HasColumnType("text");
        builder.Property(p => p.Price).HasColumnType("numeric(10,2)").HasDefaultValue(0m);
        builder.Property(p => p.Currency).HasMaxLength(10).HasDefaultValue("USD");
        builder.Property(p => p.Stock).HasDefaultValue(0);
        builder.Property(p => p.Active).HasDefaultValue(true);
        builder.Property(p => p.Metadata).HasColumnType("json");
        builder.Property(p => p.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(p => p.UpdatedAt).HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
        builder.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.SetNull);
    }
}
