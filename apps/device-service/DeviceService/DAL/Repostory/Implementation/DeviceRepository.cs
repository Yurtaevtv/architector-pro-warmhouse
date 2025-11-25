using DeviceService.DAL.Context;
using DeviceService.DAL.Entity;
using DeviceService.Models;
using Microsoft.EntityFrameworkCore;

namespace DeviceService.DAL.Repostory.Implementation
{
    internal class DeviceRepository(IDbContextFactory<DeviceContext> deviceContextFactory) : IDeviceRepository
    {

        public async Task<Device?> GetByIdAsync(int id)
        {
            await using DeviceContext context = await deviceContextFactory.CreateDbContextAsync();

            return await context.Devices.Where(d => d.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Device[]> GetAllAsync()
        {
            await using DeviceContext context = await deviceContextFactory.CreateDbContextAsync();
            return await context.Devices
                                    .Include(d => d.DeviceMetrics)
                                        .ThenInclude(dm => dm.Metric)
                                    .Include(d => d.Type)
                                    .Include(d => d.Actions)
                                    .ToArrayAsync();
        }

        public async Task<Device> AddDevice(DeviceCreateRequest deviceInfo)
        {
            using (DeviceContext context = await deviceContextFactory.CreateDbContextAsync())
            {
                IQueryable<Metric> metrics = context.Metrics.Where(m => deviceInfo.AvailableMetrics.Contains(m.Name));

                Device result = new()
                {
                    Name = deviceInfo.Name,
                    Actions =
                        await context.Actions.Where(a => deviceInfo.AvailableCommands.Contains(a.Name)).ToArrayAsync(),
                    Type = await context.DeviceTypes.Where(dt => dt.Name == deviceInfo.DeviceType).FirstAsync()
                };

                await context.Devices.AddAsync(result);

                await context.DeviceMetrics.AddRangeAsync(
                    metrics.Select(m =>
                        new DeviceMetric()
                        {
                            Device = result,
                            Metric = m,
                            Value = "-"
                        })
                );

                await context.SaveChangesAsync();
                return result;
            }
        }

    }
}
