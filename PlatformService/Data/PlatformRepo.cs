using System;
using System.Collections.Generic;
using System.Linq;
using PlatformService.Models;

namespace PlatformService.Data 
{   
    /// <summary>
    /// Provides an implementation of the <see cref="IPlatformRepo"/> interface for platform repository operations.
    /// </summary>
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformRepo"/> class using the specified database context.
        /// </summary>
        /// <param name="context">The database context to be used by the repository.</param>

        public PlatformRepo(AppDbContext context) { _context = context; }

        /// <inheritdoc />
        public void CreatePlatform(Platform platform) 
        {
            if(platform is null)
             throw new ArgumentNullException(nameof(platform)); 

            _context.Platforms.Add(platform);
        }
           
        
        /// <inheritdoc />
        public IEnumerable<Platform> GetAllPlatform() => 
            _context.Platforms.ToList();

        /// <inheritdoc />
        public Platform GetPlatformById(int id) =>
            _context.Platforms.FirstOrDefault(p => p.Id == id);

        /// <inheritdoc />
        public bool SaveChanges() => 
            _context.SaveChanges() >= 0;
    }
}