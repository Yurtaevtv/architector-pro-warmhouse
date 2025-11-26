namespace DeviceService.Api.Components.MessageBroker.Messages
{
    public class DeviceEvent
    {
        public int? DeviceId { get; init; }
        public string Event { get; init; }
    }
}
