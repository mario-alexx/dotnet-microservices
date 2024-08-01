using System.Collections.Generic;
using CommandsService.Models;

namespace CommandsService.Data 
{
    /// <summary>
    /// Defines the contract for a service that manages commands and platforms.
    /// </summary>
    public interface ICommandRepo
    {
        /// <summary>
        /// Persists any changes made to the data store.
        /// </summary>
        /// <returns>true if changes were saved successfully; otherwise, false.</returns>
        bool SaveChanges();

        #region Platforms
        /// <summary>
        /// Retrieves all platforms.
        /// </summary>
        /// <returns>An enumerable collection of platforms.</returns>
        IEnumerable<Platform> GetAllPlatforms();

        /// <summary>
        /// Creates a new platform.
        /// </summary>
        /// <param name="plat">The platform to create.</param>
        void CreatePlatform(Platform plat);

        /// <summary>
        /// Checks if a platform exists.
        /// </summary>
        /// <param name="PlatformId">The ID of the platform to check.</param>
        /// <returns>true if the platform exists; otherwise, false.</returns>
        bool PlatformExist(int PlatformId);
        
        /// <summary>
        /// Checks if an external platform exists.
        /// </summary>
        /// <param name="externalPlatformId">The ID of the external platform to check.</param>
        /// <returns>true if the external platform exists; otherwise, false.</returns>
        bool ExternalPlatformExist(int externalPlatformId);
        #endregion

        #region Commands
        /// <summary>
        /// Retrieves all commands for a specific platform.
        /// </summary>
        /// <param name="platformId">The ID of the platform whose commands are to be retrieved.</param>
        /// <returns>An enumerable collection of commands for the specified platform.</returns>
        IEnumerable<Command> GetCommandsForPlatform(int platformId);

        /// <summary>
        /// Retrieves a specific command for a specific platform.
        /// </summary>
        /// <param name="platformId">The ID of the platform whose command is to be retrieved.</param>
        /// <param name="commandId">The ID of the command to retrieve.</param>
        /// <returns>The command with the specified ID for the specified platform.</returns>
        Command GetCommand(int platformId, int commandId);

        /// <summary>
        /// Creates a new command for a specific platform.
        /// </summary>
        /// <param name="platformId">The ID of the platform to which the command belongs.</param>
        /// <param name="command">The command to create.</param>
        void CreateCommand(int platformId, Command command);
        #endregion
    }
}