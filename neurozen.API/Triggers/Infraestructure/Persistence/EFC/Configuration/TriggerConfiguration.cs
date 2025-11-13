using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using neurozen.API.Triggers.Domain.Model.Aggregates;

namespace neurozen.API.Triggers.Infraestructure.Persistence.EFC.Configuration;

public class TriggerConfiguration : IEntityTypeConfiguration<Trigger>
{
    public void Configure(EntityTypeBuilder<Trigger> builder)
    {
        // Table name
        builder.ToTable("triggers");

        // Primary Key
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        // Properties
        builder.Property(t => t.PatientId)
            .HasColumnName("patient_id")
            .IsRequired();

        builder.Property(t => t.CategoryId)
            .HasColumnName("category_id")
            .IsRequired();

        builder.Property(t => t.StressLevel)
            .HasColumnName("stress_level")
            .IsRequired();

        builder.Property(t => t.TriggerDateTime)
            .HasColumnName("trigger_date_time")
            .IsRequired();

        builder.Property(t => t.Description)
            .HasColumnName("description")
            .IsRequired()
            .HasMaxLength(1000);

        // Indexes for better query performance
        builder.HasIndex(t => t.PatientId)
            .HasDatabaseName("idx_triggers_patient_id");

        builder.HasIndex(t => t.CategoryId)
            .HasDatabaseName("idx_triggers_category_id");

        builder.HasIndex(t => t.TriggerDateTime)
            .HasDatabaseName("idx_triggers_date_time");

        builder.HasIndex(t => t.StressLevel)
            .HasDatabaseName("idx_triggers_stress_level");
    }
}