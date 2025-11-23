using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;
using WebSocketService.Components.Manager;

namespace WebSocketService.Controllers
{
    [ApiController]
    public class WebSocketController(
                    IWebSocketManager webSocketHandler,
                    IConnectionManager connectionManager) : Controller
    {

        [HttpGet("/ws")]
        public async Task Notifications()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                await webSocketHandler.HandleAsync(webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }

    }
}
