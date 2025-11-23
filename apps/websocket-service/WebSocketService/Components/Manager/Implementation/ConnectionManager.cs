using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using WebSocketService.Components.Manager;
using WebSocketService.Models;

namespace WebSocketService.Components.Manager.Implementation
{
    public class ConnectionManager(ILogger<ConnectionManager> logger) : IConnectionManager
    {
        private readonly ConcurrentDictionary<string, WebSocketConnection> _connections = [];


        public void AddConnection(WebSocket webSocket, string connectionId)
        {
            logger.LogInformation("New connection added");
            _connections[connectionId] = new WebSocketConnection
            {
                ConnectedAt = DateTime.UtcNow,
                ConnectionId = connectionId,
                Socket = webSocket
            };
        }

        public async Task RemoveConnectionAsync(string connectionId)
        {
            if (_connections.TryRemove(connectionId, out WebSocketConnection connection))
            {
                if (connection.Socket.State == WebSocketState.Open)
                {
                    await connection.Socket.CloseAsync(
                        WebSocketCloseStatus.NormalClosure,
                        "Connection closed",
                        CancellationToken.None);

                }

                logger.LogInformation("New connection {ConnectionId} removed", connectionId);
            }
        }

        public async Task SendToAllAsync(string message)
        {
            var tasks = _connections
                .Where(c => c.Value.Socket.State is WebSocketState.Open)
                .Select(c =>
                        {
                            try
                            {
                                var bytes = Encoding.UTF8.GetBytes(message);
                                return c.Value.Socket.SendAsync(
                                    new ArraySegment<byte>(bytes),
                                    WebSocketMessageType.Text,
                                    true,
                                    CancellationToken.None);
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, "Error on send message to {ConnectionId}", c.Key);
                                return Task.CompletedTask;
                            }
                        })
                .ToList();

            await Task.WhenAll(tasks);
        }

        public int GetConnectionCount()
        {
            return _connections.Count;
        }
    }
}
