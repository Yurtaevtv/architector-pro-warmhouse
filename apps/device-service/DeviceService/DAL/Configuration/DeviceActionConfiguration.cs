using DeviceService.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeviceService.DAL.Configuration
{
    public class DeviceActionConfiguration : IEntityTypeConfiguration<DeviceAction>
    {
        public void Configure(EntityTypeBuilder<DeviceAction> builder)
        {
            builder.ToTable("commands");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).HasColumnName("id");
            builder.Property(d => d.Name).IsRequired().HasColumnName("name");
        }
    }
}
