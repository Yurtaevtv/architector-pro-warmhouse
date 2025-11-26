using Telemetry.Api.Components.Clients;

namespace Telemetry.Api.Components.Store.Implementation
{
    internal class TelemetryStore(ISmartHomeClient smartHomeClient) : ITelemetryStore
    {
        private readonly ReaderWriterLock _locker = new();

        public HashSet<int> All { get; } = [];



        public async Task<bool> TryAdd(int deviceId)
        {
            if ((await smartHomeClient.TryGetSensor(deviceId))?.Status == "active")
            {
                All.Add(deviceId);
                return true;
            }

            return false;
        }

        public void Remove(int deviceId)
        {
            All.Remove(deviceId);
        }
    }
}
