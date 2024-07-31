using System.ComponentModel.DataAnnotations;

namespace CommandsService.Models
{
    /// <summary>
    /// Represents a command entity with an ID, instructions on how to execute, the command line to be executed,
    /// and a reference to the platform it belongs to.
    /// </summary>
    public class Command 
    {
        /// <summary>
        /// Gets or sets the unique identifier for the command.
        /// </summary>
        /// <value>The unique identifier for the command.</value>
        [Key]
        [Required]
        public int Id { get; set; }

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

        /// <summary>
        /// Gets or sets the identifier of the platform to which the command belongs.
        /// </summary>
        /// <value>The identifier of the platform to which the command belongs.</value>
        [Required]
        public int PlatformId { get; set; }

        /// <summary>
        /// Gets or sets the platform entity to which the command belongs.
        /// </summary>
        /// <value>The platform entity to which the command belongs.</value>
        public Platform Platform { get; set; }
    }
}