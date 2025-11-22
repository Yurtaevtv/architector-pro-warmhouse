namespace DeviceService.Components.MessageBroker
{
    public interface IDeviceEventPublisher
    {
        /// <summary>
        /// publish event to essage broker
        /// </summary>
        /// <param name="event">deviec service event</param>
        Task PublishAsync(object @event);

    }
}
