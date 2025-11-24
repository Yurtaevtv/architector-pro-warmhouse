using Microsoft.Extensions.Options;
using System.Text.Json;
using WebSocketService.Components.Manager;
using WebSocketService.Models;

namespace WebSocketService.Components.Kafka.implementation
{
    public class DeviceEventConsumer(
                    IConnectionManager connectionManager,
                    IOptions<KafkaSettings> settings,
                    ILogger<EventConsumer> logger) : EventConsumer(settings, logger)
    {

        protected override string Topic => _settings.Value.DeviceEventsTopic!;

        protected override async Task ProcessKafkaMessage(string message)
        {
            try
            {
                // Отправляем всем подключенным WebSocket клиентам
                await connectionManager.SendToAllAsync(message);

                _logger.LogInformation("Message forwarded to WebSocket clients. Topic: {Topic}, Connections: {Connections}",
                    Topic, connectionManager.GetConnectionCount());
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Failed to parse Kafka message from topic: {Topic}", Topic);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process Kafka message from topic: {Topic}", Topic);
            }
        }
    }
}
