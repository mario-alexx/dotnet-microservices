using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommandsService.Models
{
    /// <summary>
    /// Represents a platform entity with an ID, external ID, name, and a collection of commands.
    /// </summary>
    public class Platform 
    {
        /// <summary>
        /// Gets or sets the unique identifier for the platform.
        /// </summary>
        /// <value>The unique identifier for the platform.</value>/// <summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the external identifier for the platform.
        /// </summary>
        /// <value>The external identifier for the platform.</value>/// <summary>
        [Required]
        public int ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the name of the platform.
        /// </summary>
        /// <value>The name of the platform.</value>/// <summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the collection of commands associated with the platform.
        /// </summary>
        /// <value>A collection of commands associated with the platform.</value>
        public ICollection<Command>  Commands { get; set; } = new List<Command>();
    }
}


