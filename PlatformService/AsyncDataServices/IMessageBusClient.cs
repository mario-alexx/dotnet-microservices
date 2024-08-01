using PlatformService.Dtos;

namespace PlatformService.AsynDataServices
{
    /// <summary>
    /// Provides an interface for message bus client operations.
    /// </summary>
    public interface IMessageBusClient
    {
        /// <summary>
        /// Publishes a new platform event to the message bus.
        /// </summary>
        /// <param name="platformPublishedDto">The platform published DTO containing event data.</param>
        void PublishNewPlatform(PlatformPublishedDto platformPublishedDto);
    }
}