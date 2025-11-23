namespace DeviceService.DAL.Entity
{
    public class DeviceAction
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public ICollection<Device> Devices { get; init; }
    }
}
