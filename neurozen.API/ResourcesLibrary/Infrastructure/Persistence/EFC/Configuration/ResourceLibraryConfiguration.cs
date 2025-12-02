using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using neurozen.API.ResourcesLibrary.Domain.Model.Aggregates;

namespace neurozen.API.ResourcesLibrary.Infrastructure.Persistence.EFC.Configuration;

public class ResourceLibraryConfiguration : IEntityTypeConfiguration<ResourceLibrary>
{
    public void Configure(EntityTypeBuilder<ResourceLibrary> builder)
    {
        // Table name
        builder.ToTable("resource_libraries");

        // Primary Key
        builder.HasKey(rl => rl.Id);
        builder.Property(rl => rl.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        // Properties
        builder.Property(rl => rl.Title)
            .HasColumnName("title")
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(rl => rl.Description)
            .HasColumnName("description")
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(rl => rl.ResourceType)
            .HasColumnName("resource_type")
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(rl => rl.ContentUrl)
            .HasColumnName("content_url")
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(rl => rl.Duration)
            .HasColumnName("duration")
            .IsRequired();

        builder.Property(rl => rl.Author)
            .HasColumnName("author")
            .IsRequired()
            .HasMaxLength(100);

        // Indexes
        builder.HasIndex(rl => rl.ResourceType)
            .HasDatabaseName("idx_resource_library_type");

        builder.HasIndex(rl => rl.Title)
            .HasDatabaseName("idx_resource_library_title");
    }
}
