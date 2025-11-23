using DeviceService.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeviceService.DAL.Configuration
{
    public class DeviceTypeConfiguration : IEntityTypeConfiguration<DeviceType>
    {
        public void Configure(EntityTypeBuilder<DeviceType> builder)
        {
            builder.ToTable("device_types");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).HasColumnName("id");
            builder.Property(d => d.Name).IsRequired().HasColumnName("name");
        }
    }
}
