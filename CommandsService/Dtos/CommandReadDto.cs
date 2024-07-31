namespace CommandsService.Dtos 
{
    /// <summary>
    /// Data Transfer Object (DTO) for reading command information.
    /// </summary>
    public class CommandReadDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the command.
        /// </summary>
        /// <value>The unique identifier for the command.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the instructions on how to execute the command.
        /// </summary>
        /// <value>The instructions on how to execute the command.</value>
        public string HowTo { get; set; }

        /// <summary>
        /// Gets or sets the command line to be executed.
        /// </summary>
        /// <value>The command line to be executed.</value>
        public string CommandLine { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the platform to which the command belongs.
        /// </summary>
        /// <value>The identifier of the platform to which the command belongs.</value>
        public int PlatformId { get; set; }
    }
}