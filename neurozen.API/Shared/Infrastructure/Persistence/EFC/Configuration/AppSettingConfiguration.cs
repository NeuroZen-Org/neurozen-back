using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using neurozen.API.Shared.Domain.Entities;

namespace neurozen.API.Shared.Infrastructure.Persistence.EFC.Configuration
{
    public class AppSettingConfiguration : IEntityTypeConfiguration<AppSetting>
    {
        public void Configure(EntityTypeBuilder<AppSetting> builder)
        {
            builder.ToTable("app_settings");
            builder.HasKey(a => a.Key);
            builder.Property(a => a.Key).HasMaxLength(150);
            builder.Property(a => a.Value).HasColumnType("json");
            builder.Property(a => a.UpdatedAt).HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
