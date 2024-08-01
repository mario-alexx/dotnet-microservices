namespace PlatformService.Dtos 
{
    /// <summary>
    /// Data Transfer Object (DTO) for published platform events.
    /// </summary>
    public class PlatformPublishedDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the platform.
        /// </summary>
        /// <value>The unique identifier for the platform.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the platform.
        /// </summary>
        /// <value>The name of the platform.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the event type.
        /// </summary>
        /// <value>The event type.</value>
        public string Event { get; set; }
    }
} 