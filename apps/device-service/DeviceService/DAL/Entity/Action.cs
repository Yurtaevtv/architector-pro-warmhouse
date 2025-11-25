namespace DeviceService.DAL.Entity
{
    public class Action
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public ICollection<Device> Devices { get; init; }
    }
}
