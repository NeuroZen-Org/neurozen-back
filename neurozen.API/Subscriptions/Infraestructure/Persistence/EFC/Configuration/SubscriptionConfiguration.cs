using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using neurozen.API.Subscriptions.Domain.Model.Aggregates;

namespace neurozen.API.Subscriptions.Infraestructure.Persistence.EFC.Configuration;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        // Table name
        builder.ToTable("Subscriptions");
        
        // Primary Key
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd();
        
        // Properties
        builder.Property(t => t.PlanId)
            .IsRequired()
            .HasColumnName("PlanId");
        
        builder.Property(t => t.NameUser)
            .IsRequired()
            .HasColumnName("NameUser");
        
        builder.Property(t => t.LastNameUser)
            .IsRequired()
            .HasColumnName("LastNameUser");
        
        builder.Property(t => t.EmailUser)
            .IsRequired()
            .HasColumnName("EmailUser");
        
        builder.Property(t => t.NumberCard)
            .IsRequired()
            .HasColumnName("NumberCard");
        
        builder.Property(t => t.ExpirationDate)
            .IsRequired()
            .HasColumnName("ExpirationDate");
        
        builder.Property(t => t.Cvv)
            .IsRequired()
            .HasColumnName("Cvv");
        
        // 👇 Nueva propiedad opcional
        builder.Property(t => t.IsActive)
            .HasColumnName("IsActive")
            .HasDefaultValue(true); // puedes poner false o null según tu lógica
        
        // Indexes for better performance
        builder.HasIndex(t => t.PlanId).HasDatabaseName("IX_Subscription_PlanId");
        builder.HasIndex(t => t.EmailUser).HasDatabaseName("IX_Subscription_EmailUser");
        builder.HasIndex(t => t.NumberCard).HasDatabaseName("IX_Subscription_NumberCard");
        builder.HasIndex(t => t.ExpirationDate).HasDatabaseName("IX_Subscription_ExpirationDate");
        builder.HasIndex(t => t.Cvv).HasDatabaseName("IX_Subscription_Cvv");
        builder.HasIndex(t => t.IsActive).HasDatabaseName("IX_Subscription_IsActive"); // 👈 opcional
    }
}
