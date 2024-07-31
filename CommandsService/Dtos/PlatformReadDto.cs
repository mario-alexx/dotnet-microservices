namespace CommandsService.Dtos 
{
    /// <summary>
    /// Data Transfer Object (DTO) for reading platform information.
    /// </summary>
    public class PlatformReadDto
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
    }
}