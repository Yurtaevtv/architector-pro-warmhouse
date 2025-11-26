namespace Telemetry.Api.Components.Store
{
    public interface ITelemetryStore
    {
        HashSet<int> All { get; }

        /// <summary>
        /// Add device id for telemtry monitoring
        /// </summary>
        /// <param name="deviceId"></param>
        Task<bool> TryAdd(int deviceId);

        void Remove(int deviceId);
    }
}
