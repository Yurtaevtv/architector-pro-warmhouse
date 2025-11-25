
namespace DeviceService.Models
{
    public record DeviceInfo
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string DeviceType { get; set; }

        public string Status { get; set; }

        public Dictionary<string, string> Metrics { get; set; }

        public string[] Actions { get; set; }
    }
}
