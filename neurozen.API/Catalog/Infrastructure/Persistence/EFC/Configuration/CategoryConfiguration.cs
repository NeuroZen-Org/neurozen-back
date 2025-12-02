﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using neurozen.API.Catalog.Domain.Entities;

namespace neurozen.API.Catalog.Infrastructure.Persistence.EFC.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(150);
        builder.Property(c => c.Slug).IsRequired().HasMaxLength(150);
        builder.HasIndex(c => c.Slug).IsUnique();
        builder.Property(c => c.Description).HasColumnType("text");
        builder.Property(c => c.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(c => c.UpdatedAt).HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
        builder.HasOne(c => c.Parent).WithMany(p => p.Children).HasForeignKey(c => c.ParentId).OnDelete(DeleteBehavior.SetNull);
    }
}
