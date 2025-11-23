using System.Net.WebSockets;

namespace WebSocketService.Components.Manager
{
    public interface IWebSocketManager
    {
        Task HandleAsync(WebSocket socket);
    }
}
