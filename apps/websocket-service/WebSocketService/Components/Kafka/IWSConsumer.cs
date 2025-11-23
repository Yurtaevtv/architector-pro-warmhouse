namespace WebSocketService.Components.Kafka
{
    public interface IWSConsumer : IDisposable
    {

        Task StartConsumingAsync();

        Task StopConsumersAsync();
    }
}
