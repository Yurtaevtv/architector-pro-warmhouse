using DeviceService.DAL.Entity;
using DeviceService.Models.Settings;
using Microsoft.EntityFrameworkCore;

namespace DeviceService.DAL.Context
{
    public sealed class DeviceContext : DbContext
    {
        private IConfiguration _config;



        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<DeviceMetric> Metrics { get; set; }
        public DbSet<DeviceAction> Actions { get; set; }


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
                    r => r.HasOne(typeof(DeviceAction)).WithMany().HasForeignKey("command_id").HasPrincipalKey(nameof(DeviceAction.Id)),
                    l => l.HasOne(typeof(Device)).WithMany().HasForeignKey("device_id").HasPrincipalKey(nameof(Device.Id)),
                    j => j.HasKey("command_id", "device_id"));

            modelBuilder.Entity<Device>()
                .HasMany(d => d.AvailableMetrics)
                .WithMany(c => c.Devices)
                .UsingEntity("device_metrics",
                    r => r.HasOne(typeof(DeviceMetric)).WithMany().HasForeignKey("metric_id").HasPrincipalKey(nameof(DeviceMetric.Id)),
                    l => l.HasOne(typeof(Device)).WithMany().HasForeignKey("device_id").HasPrincipalKey(nameof(Device.Id)),
                    j => j.HasKey("metric_id", "device_id"));

            modelBuilder.Entity<Device>()
                .HasOne(d => d.Type)
                .WithMany(c => c.Devices)
                .HasForeignKey(d => d.TypeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
