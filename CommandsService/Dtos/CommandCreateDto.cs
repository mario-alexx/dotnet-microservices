using System.ComponentModel.DataAnnotations;

namespace CommandsService.Dtos 
{
    /// <summary>
    /// Data Transfer Object (DTO) for creating a new command.
    /// </summary>
    public class CommandCreateDto
    {
        /// <summary>
        /// Gets or sets the instructions on how to execute the command.
        /// </summary>
        /// <value>The instructions on how to execute the command.</value>
        [Required]
        public string HowTo { get; set; }

        /// <summary>
        /// Gets or sets the command line to be executed.
        /// </summary>
        /// <value>The command line to be executed.</value>
        [Required]
        public string CommandLine { get; set; }
    }
}