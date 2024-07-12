using System.Collections.Generic;
using PlatformService.Models;

namespace PlatformService.Data
{
    /// <summary>
    /// Provides an interface for platform repository operations.
    /// </summary>
    public interface IPlatformRepo
    {
        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <returns>True if the changes were successfully saved to the database; otherwise, false.</returns>
        bool SaveChanges();

        /// <summary>
        /// Gets all platform entities from the repository.
        /// </summary>
        /// <returns>An enumerable collection of platform entities.</returns>
        IEnumerable<Platform> GetAllPlatform();
        
        /// <summary>
        /// Gets a platform entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the platform entity to retrieve.</param>
        /// <returns>The platform entity with the specified identifier, or null if not found.</returns>
        Platform GetPlatformById(int id);
        
        /// <summary>
        /// Creates a new platform entity in the repository.
        /// </summary>
        /// <param name="platform">The platform entity to create.</param>
        void CreatePlatform(Platform platform);   
    }
}