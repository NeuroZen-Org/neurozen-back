using Microsoft.EntityFrameworkCore;
using neurozen.API.IAM.Domain.Model.Aggregates;

namespace neurozen.API.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        // IAM Context
        
        builder.Entity<User>().ToTable("users"); // ✅ Especificar nombre de tabla
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Username).IsRequired();
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
        
        // Profile fields - opcionales
        builder.Entity<User>().Property(u => u.Email).HasMaxLength(255).IsRequired(false);
        builder.Entity<User>().Property(u => u.FullName).HasMaxLength(255).IsRequired(false);
        builder.Entity<User>().Property(u => u.PhoneNumber).HasMaxLength(50).IsRequired(false);
        builder.Entity<User>().Property(u => u.Address).HasMaxLength(500).IsRequired(false);
        builder.Entity<User>().Property(u => u.AvatarUrl).HasMaxLength(500).IsRequired(false);
        builder.Entity<User>().Property(u => u.DateOfBirth).IsRequired(false);
        
        // Audit fields - con valores por defecto generados por la base de datos
        builder.Entity<User>()
            .Property(u => u.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
            .ValueGeneratedOnAdd()
            .IsRequired(false); // Permitir null temporalmente por compatibilidad
            
        builder.Entity<User>()
            .Property(u => u.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
            .ValueGeneratedOnAddOrUpdate()
            .IsRequired(false); // Permitir null temporalmente por compatibilidad
    }
}