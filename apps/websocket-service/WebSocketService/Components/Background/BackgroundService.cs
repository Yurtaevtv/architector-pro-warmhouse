using System.Threading;
using WebSocketService.Components.Kafka;

namespace WebSocketService.Components.Background
{
    public class ConsumerBackgroundService(IEnumerable<IWSConsumer> consumers) : BackgroundService
    {

        CancellationTokenSource _cancellationTokenSource;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);
            return Task.WhenAll(consumers.Select(c => Task.Run(() => c.StartConsumingAsync(_cancellationTokenSource), stoppingToken)));
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _cancellationTokenSource.Cancel();
            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            foreach (IWSConsumer consumer in consumers)
            {
                consumer.Dispose();
            }
            GC.SuppressFinalize(this);
            base.Dispose();
        }
    }
}