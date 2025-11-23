using System.Net.WebSockets;

namespace WebSocketService.Models
{
    public class WebSocketConnection
    {
        public string ConnectionId { get; set; } = string.Empty;
        public WebSocket Socket { get; set; } = null!;
        public DateTime ConnectedAt { get; set; } = DateTime.UtcNow;
    }
}
