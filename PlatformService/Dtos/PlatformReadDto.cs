namespace PlatformService.Dtos 
{
    /// <summary>
    /// Data Transfer Object (DTO) for reading platform information.
    /// </summary>
    public class PlatformReadDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the platform.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the platform.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the publisher of the platform.
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// Gets or sets the cost of the platform.
        /// </summary>
        public string Cost { get; set; }
    }
}