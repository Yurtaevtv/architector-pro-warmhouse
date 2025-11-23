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
            return await context.Devices.ToArrayAsync();
        }

        public async Task<Device> AddDevice(DeviceMetadata deviceInfo)
        {
            await using DeviceContext context = await deviceContextFactory.CreateDbContextAsync();
            Device result = new()
            {
                Name = deviceInfo.Name,
                Actions =
                    await context.Actions.Where(a => deviceInfo.AvailableCommands.Contains(a.Name)).ToArrayAsync(),
                AvailableMetrics = await context.Metrics.Where(m => deviceInfo.AvailableMetrics.Contains(m.Name))
                    .ToArrayAsync(),
                Type = await context.DeviceTypes.Where(dt => dt.Name == deviceInfo.DeviceType).FirstAsync()
            };
            await context.Devices.AddAsync(result);
            await context.SaveChangesAsync();
            return result;
        }

    }
}
