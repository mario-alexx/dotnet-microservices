using CommandsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Data 
{
    /// <summary>
    /// Represents the database context for the application, providing access to the Platform and Command entities.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class with specified options.
        /// </summary>
        /// <param name="opt">The options to be used by the DbContext.</param>
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
        }

        /// <summary>
        /// Gets or sets the DbSet for Platform entities.
        /// </summary>
        /// <value>The DbSet for Platform entities.</value>
        public DbSet<Platform> Platforms { get; set; }
        /// <summary>
        /// Gets or sets the DbSet for Command entities.
        /// </summary>
        /// <value>The DbSet for Command entities.</value>
        public DbSet<Command> Commands { get; set; }
        
        /// <summary>
        /// Configures the model that was discovered by convention from the entity types exposed in DbSet properties on your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Platform>()
                .HasMany(p => p.Commands)
                .WithOne(p => p.Platform!)
                .HasForeignKey(p => p.PlatformId);

            modelBuilder
                .Entity<Command>()
                .HasOne(p => p.Platform)
                .WithMany(p => p.Commands)
                .HasForeignKey(p => p.PlatformId);
        }
    }
}