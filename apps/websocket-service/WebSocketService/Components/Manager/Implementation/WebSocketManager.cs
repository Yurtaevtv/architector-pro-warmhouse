using System.Net.WebSockets;
using System.Text;

namespace WebSocketService.Components.Manager.Implementation
{
    public class WSSocketManager(
                        IConnectionManager connectionManager,
                        ILogger<WSSocketManager> logger) : IWebSocketManager
    {

        private async Task ReceiveAsync(WebSocket socket, string conncetionId)
        {
            var buffer = new ArraySegment<byte>(new byte[4096]);

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer, CancellationToken.None);

                switch (result.MessageType)
                {
                    case WebSocketMessageType.Text:
                        var message = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
                        logger.LogInformation("WebSocket serviec received message: {Message}", message);
                        break;

                    case WebSocketMessageType.Binary:
                        logger.LogInformation("WebSocket serviec received binary info");
                        break;

                    case WebSocketMessageType.Close:
                        await socket.CloseAsync(
                            result.CloseStatus.Value,
                            result.CloseStatusDescription,
                            CancellationToken.None
                        );
                        break;
                }
            }
        }


        public async Task HandleAsync(WebSocket socket)
        {
            var connectionId = Guid.NewGuid().ToString("N");

            connectionManager.AddConnection(socket, connectionId);

            try
            {
                await ReceiveAsync(socket, connectionId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in WebSocket connection {connectionId}");
            }
            finally
            {
                await connectionManager.RemoveConnectionAsync(connectionId);
                logger.LogInformation($"WebSocket connection closed: {connectionId}");
            }
        }
    }
}
