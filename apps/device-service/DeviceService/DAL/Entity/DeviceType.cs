namespace DeviceService.DAL.Entity
{
    public class DeviceType
    {
        public int Id { get; init; }
        public string Name { get; init; }

        public ICollection<Device> Devices { get; init; }
    }
}
