using System.ComponentModel.DataAnnotations;

namespace Telemetry.Api.Models.Settings
{
    public class AppSettings
    {
        [Required]
        [Url]
        public string? SmartHomeUrl { get; init; }

        /// <summary>
        /// Delay telemetry 
        /// </summary>
        public string Schedule { get; init; } = "* * * * * *";
    }
}
