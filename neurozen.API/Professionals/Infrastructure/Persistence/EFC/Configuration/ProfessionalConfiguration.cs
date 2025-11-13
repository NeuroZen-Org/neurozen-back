using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using neurozen.API.Professionals.Domain.Model.Aggregates;

namespace neurozen.API.Professionals.Infrastructure.Persistence.EFC.Configuration;

public class ProfessionalConfiguration : IEntityTypeConfiguration<Professional>
{
    public void Configure(EntityTypeBuilder<Professional> builder)
    {
        builder.ToTable("Professionals");

        // Primary key
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd();

        // Name
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("Name");

        // Specialty
        builder.Property(p => p.Specialty)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("Specialty");

        // Experience
        builder.Property(p => p.Experience)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("Experience");

        // Rating
        builder.Property(p => p.Rating)
            .HasColumnName("Rating");

        // Reviews
        builder.Property(p => p.Reviews)
            .HasColumnName("Reviews");

        // Price
        builder.Property(p => p.Price)
            .HasColumnName("Price");

        // Availability
        builder.Property(p => p.Availability)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("Availability");

        // Bio
        builder.Property(p => p.Bio)
            .IsRequired()
            .HasColumnName("Bio");

        // Image
        builder.Property(p => p.Image)
            .IsRequired()
            .HasColumnName("Image");
    }
}