using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;
using WebSocketService.Components.Manager;

namespace WebSocketService.Controllers
{
    [ApiController]
    public class WebSocketController(
                    IWebSocketManager webSocketHandler,
                    IConnectionManager connectionManager,
                    ILogger<WebSocketController> logger) : Controller
    {

        [HttpGet("/ws")]
        public async Task Notifications()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                logger.LogInformation("WebSocket connection");
                var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                await webSocketHandler.HandleAsync(webSocket);
            }
            else
            {
                logger.LogInformation("WebSocket was not connected");
                HttpContext.Response.StatusCode = 400;
            }
        }

    }
}
