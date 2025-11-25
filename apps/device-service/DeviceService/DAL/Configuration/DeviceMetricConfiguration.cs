using DeviceService.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeviceService.DAL.Configuration
{
    public class DeviceMetricConfiguration : IEntityTypeConfiguration<Metric>
    {
        public void Configure(EntityTypeBuilder<Metric> builder)
        {
            builder.ToTable("metrics");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).HasColumnName("id");
            builder.Property(d => d.Name).IsRequired().HasColumnName("name");
        }
    }
}
