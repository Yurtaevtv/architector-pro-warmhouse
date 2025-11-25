using DeviceService.DAL.Entity;
using DeviceService.Models.Settings;
using Microsoft.EntityFrameworkCore;
using Action = DeviceService.DAL.Entity.Action;

namespace DeviceService.DAL.Context
{
    public sealed class DeviceContext : DbContext
    {
        private IConfiguration _config;



        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceMetric> DeviceMetrics { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<Metric> Metrics { get; set; }
        public DbSet<Action> Actions { get; set; }


        public DeviceContext(IConfiguration config)
        {
            _config = config;

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_config.GetConnectionString(AppSettings.ConnectionStringKey));

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeviceContext).Assembly);
            modelBuilder.Entity<Device>()
                .HasMany(d => d.Actions)
                .WithMany(c => c.Devices)
                .UsingEntity("device_commands",
                    r => r.HasOne(typeof(Action)).WithMany().HasForeignKey("command_id").HasPrincipalKey(nameof(Action.Id)),
                    l => l.HasOne(typeof(Device)).WithMany().HasForeignKey("device_id").HasPrincipalKey(nameof(Device.Id)),
                    j => j.HasKey("command_id", "device_id"));

            modelBuilder.Entity<Device>()
                .HasMany(d => d.Metrics)
                .WithMany(c => c.Devices)
                .UsingEntity<DeviceMetric>(
                    l => l
                        .HasOne(pt => pt.Metric)
                        .WithMany(pt => pt.DeviceMetrics)
                        .HasForeignKey(pt => pt.MetricId),
                    r => r
                            .HasOne(pt => pt.Device)
                            .WithMany(pt => pt.DeviceMetrics)
                            .HasForeignKey(pt => pt.DeviceId),
                    j =>
                    {
                        j.HasKey(pt => new { pt.DeviceId, pt.MetricId });
                        j.Property(pt => pt.Value).HasColumnName("value");
                        j.Property(pt => pt.DeviceId).HasColumnName("device_id");
                        j.Property(pt => pt.MetricId).HasColumnName("metric_id");
                        j.ToTable("device_metrics");
                    });

            modelBuilder.Entity<Device>()
                .HasOne(d => d.Type)
                .WithMany(c => c.Devices)
                .HasForeignKey(d => d.TypeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
