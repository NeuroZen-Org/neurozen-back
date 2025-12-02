﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using neurozen.API.Payments.Domain.Entities;

namespace neurozen.API.Payments.Infrastructure.Persistence.EFC.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("payments");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Provider).HasMaxLength(100);
            builder.Property(p => p.Amount).HasColumnType("numeric(12,2)");
            builder.Property(p => p.Currency).HasMaxLength(10);
            builder.Property(p => p.Status).HasMaxLength(50);
            builder.Property(p => p.ProviderResponse).HasColumnType("json");
            builder.Property(p => p.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.HasOne(p => p.Order).WithMany(o => o.Payments).HasForeignKey(p => p.OrderId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
