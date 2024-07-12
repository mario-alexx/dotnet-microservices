using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data 
{
    /// <summary>
    /// A static class responsible for preparing the database.
    /// </summary>
    public static class PrepDb 
    {
        /// <summary>
        /// Prepares the population of the database.
        /// </summary>
        /// <param name="app">An application builder used to configure the application's request pipeline.</param>
        public static void PrepPopulation(IApplicationBuilder app) 
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        /// <summary>
        /// Seeds the database with initial data.
        /// </summary>
        /// <param name="context">The database context used to interact with the database.</param>
        private static void SeedData(AppDbContext context) 
        {
            if(!context.Platforms.Any()) 
            {
                Console.WriteLine("--> Seeding Data...");

                context.Platforms.AddRange(
                    new Platform() 
                    {
                        Name = "Dot net",
                        Publisher = "Microsoft",
                        Cost = "Free",
                    },
                    new Platform() 
                    {
                        Name = "SQL Server Express",
                        Publisher = "Microsoft",
                        Cost = "Free",
                    },
                    new Platform() 
                    {
                        Name = "Kubernetes",
                        Publisher = "Cloud Native Computing Foundation",
                        Cost = "Free",
                    }
                );

                context.SaveChanges();
            }
            else 
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}