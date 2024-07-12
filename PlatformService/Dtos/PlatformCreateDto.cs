using System.ComponentModel.DataAnnotations;

namespace PlatformService.Dtos 
{
    /// <summary>
    /// Data Transfer Object (DTO) for creating platform information.
    /// </summary>
    public class PlatformCreateDto
    {
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