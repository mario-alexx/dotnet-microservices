using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data 
{
    /// <summary>
    /// Represents the application's database context.
    /// </summary>
    public class AppDbContext : DbContext 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class using the specified options.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { }

        /// <summary>
        /// Gets or sets the Platforms table in the database.
        /// </summary>
        public DbSet<Platform> Platforms { get; set; }
    }
}