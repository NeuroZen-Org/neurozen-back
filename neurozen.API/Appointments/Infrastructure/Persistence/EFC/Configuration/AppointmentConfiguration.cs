using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using neurozen.API.Appointments.Domain.Model.Aggregates;

namespace neurozen.API.Appointments.Infrastructure.Persistence.EFC.Configuration;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        // Table name
        builder.ToTable("appointments");

        // Primary Key
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        // Properties
        builder.Property(a => a.PatientId)
            .HasColumnName("patient_id")
            .IsRequired();

        builder.Property(a => a.ProfessionalId)
            .HasColumnName("professional_id")
            .IsRequired();

        builder.Property(a => a.AppointmentDateTime)
            .HasColumnName("appointment_date_time")
            .IsRequired();

        // Indexes for better query performance
        builder.HasIndex(a => a.PatientId)
            .HasDatabaseName("idx_appointments_patient_id");

        builder.HasIndex(a => a.ProfessionalId)
            .HasDatabaseName("idx_appointments_professional_id");

        builder.HasIndex(a => a.AppointmentDateTime)
            .HasDatabaseName("idx_appointments_date_time");
    }
}
