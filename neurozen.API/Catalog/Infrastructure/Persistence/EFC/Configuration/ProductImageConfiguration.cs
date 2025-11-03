using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using neurozen.API.Catalog.Domain.Entities;

namespace neurozen.API.Catalog.Infrastructure.Persistence.EFC.Configuration;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable("product_images");
        builder.HasKey(pi => pi.Id);
        builder.Property(pi => pi.Url).IsRequired().HasColumnType("text");
        builder.Property(pi => pi.Alt).HasColumnType("text");
        builder.Property(pi => pi.Position).HasDefaultValue(0);
        builder.HasOne(pi => pi.Product).WithMany(p => p.Images).HasForeignKey(pi => pi.ProductId).OnDelete(DeleteBehavior.Cascade);
    }
}
