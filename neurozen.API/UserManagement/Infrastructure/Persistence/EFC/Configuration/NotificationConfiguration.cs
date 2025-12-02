﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using neurozen.API.UserManagement.Domain.Entities;

namespace neurozen.API.UserManagement.Infrastructure.Persistence.EFC.Configuration;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("notifications");
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Type).HasMaxLength(100);
        builder.Property(n => n.Title).HasMaxLength(255);
        builder.Property(n => n.Body).HasColumnType("text");
        builder.Property(n => n.Read).HasDefaultValue(false);
        builder.Property(n => n.Meta).HasColumnType("json");
        builder.Property(n => n.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.HasOne(n => n.User).WithMany(u => u.Notifications).HasForeignKey(n => n.UserId).OnDelete(DeleteBehavior.Cascade);
    }
}
