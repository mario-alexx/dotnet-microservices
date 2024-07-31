using System;
using System.Collections.Generic;
using System.Linq;
using CommandsService.Models;

namespace CommandsService.Data 
{
    /// <summary>
    /// Provides implementation for the <see cref="ICommandService"/> to manage commands and platforms.
    /// </summary>
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandRepo"/> class with the specified database context.
        /// </summary>
        /// <param name="context">The database context to be used by this repository.</param>
        public CommandRepo(AppDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public void CreateCommand(int platformId, Command command)
        {
            if(command is null) 
            {
                throw new ArgumentNullException(nameof(command));
            }
            command.PlatformId = platformId;
            _context.Commands.Add(command);
        }

        /// <inheritdoc/>
        public void CreatePlatform(Platform plat)
        {
            if(plat is null) 
            {
                throw new ArgumentNullException(nameof(plat));
            }

            _context.Platforms.Add(plat);
        }

        /// <inheritdoc/>
        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        /// <inheritdoc/>
        public Command GetCommand(int platformId, int commandId)
        {
            return _context.Commands
                .Where(c => c.PlatformId == platformId && c.Id == commandId)
                .FirstOrDefault();
        }

        /// <inheritdoc/>
        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            return _context.Commands
                .Where(c => c.PlatformId == platformId)
                .OrderBy(c => c.Platform.Name);
        }

        /// <inheritdoc/>
        public bool PlatformExist(int PlatformId)
        {
            return _context.Platforms.Any(p => p.Id == PlatformId);
        }

        /// <inheritdoc/>
        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}