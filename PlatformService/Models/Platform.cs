using System.ComponentModel.DataAnnotations;

namespace PlatformService.Models
{
    /// <summary>
    /// Represents a platform entity.
    /// </summary>
    public class Platform
    {
        /// <summary>
        /// Gets or sets the unique identifier of the platform.
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the platform.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the publisher of the platform.
        /// </summary>
        [Required]
        public string Publisher { get; set; }

        /// <summary>
        /// Gets or sets the cost of the platform.
        /// </summary>
        [Required]
        public string Cost { get; set; }
    }
}
