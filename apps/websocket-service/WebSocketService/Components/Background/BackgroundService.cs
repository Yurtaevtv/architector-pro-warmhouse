using WebSocketService.Components.Kafka;

namespace WebSocketService.Components.Background
{
    public class ConsumerBackgroundService(IEnumerable<IWSConsumer> consumers) : IHostedService, IDisposable
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.WhenAll(consumers.Select(c => c.StartConsumingAsync()));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.WhenAll(consumers.Select(c => c.StopConsumersAsync()));
        }

        public void Dispose()
        {
            foreach (IWSConsumer consumer in consumers)
            {
                consumer.Dispose();
            }
        }
    }
}