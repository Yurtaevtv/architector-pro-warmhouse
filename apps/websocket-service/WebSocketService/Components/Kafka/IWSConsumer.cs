using System.Threading;

namespace WebSocketService.Components.Kafka
{
    public interface IWSConsumer : IDisposable
    {

        Task StartConsumingAsync(CancellationTokenSource cts);
    }
}
