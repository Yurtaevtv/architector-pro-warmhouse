using DeviceService.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeviceService.DAL.Configuration
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("devices");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).HasColumnName("id");
            builder.Property(d => d.Name).IsRequired().HasColumnName("name");
            builder.Property(d => d.DeviceUrl).HasColumnName("device_url");
            builder.Property(d => d.TypeId).IsRequired().HasColumnName("device_type_id");
        }
    }
}
