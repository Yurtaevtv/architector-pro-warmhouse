using System.Net.WebSockets;

namespace WebSocketService.Components.Manager
{
    public interface IConnectionManager
    {

        void AddConnection(WebSocket webSocket, string connectionId);
        Task RemoveConnectionAsync(string connectionId);

        Task SendToAllAsync(string message);

        int GetConnectionCount();
    }
}
