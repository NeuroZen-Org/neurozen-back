﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using neurozen.API.UserManagement.Domain.Entities;

namespace neurozen.API.UserManagement.Infrastructure.Persistence.EFC.Configuration;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("sessions");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Token).IsRequired().HasMaxLength(255);
        builder.HasIndex(s => s.Token).IsUnique();
        builder.Property(s => s.Ip).HasMaxLength(50);
        builder.Property(s => s.UserAgent).HasColumnType("text");
        builder.Property(s => s.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.HasOne(s => s.User).WithMany(u => u.Sessions).HasForeignKey(s => s.UserId).OnDelete(DeleteBehavior.Cascade);
    }
}
