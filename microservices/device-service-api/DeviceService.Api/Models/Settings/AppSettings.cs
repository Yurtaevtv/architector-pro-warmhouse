using System.ComponentModel.DataAnnotations;

namespace DeviceService.Api.Models.Settings
{
    public class AppSettings
    {
        [Required]
        [Url]
        public string? SmartHomeUrl { get; init; }
    }
}
