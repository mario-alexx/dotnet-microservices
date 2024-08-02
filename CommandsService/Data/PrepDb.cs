using System;
using System.Collections.Generic;
using CommandsService.Data;
using CommandsService.Models;
using CommandsService.SyncDataService.Grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CommandsService
{   
    /// <summary>
    /// Provides methods for preparing and seeding the database.
    /// </summary>
    public static class PrepDb 
    {
        /// <summary>
        /// Prepares the database population by seeding initial data.
        /// </summary>
        /// <param name="applicationBuilder">The application builder used to configure the app.</param>
        public static void PrepPopulation(IApplicationBuilder applicationBuilder) 
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) 
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();
                
                var platforms = grpcClient.ReturnAllPlatforms();

                SeedData(serviceScope.ServiceProvider.GetService<ICommandRepo>(), platforms);
            }
        }

        /// <summary>
        /// Seeds the database with initial data.
        /// </summary>
        /// <param name="repo">The command repository to interact with the database.</param>
        /// <param name="platforms">The list of platforms to seed.</param>
        private static void SeedData(ICommandRepo repo, IEnumerable<Platform> platforms) 
        {
            Console.WriteLine("--> Seeding new Platforms...");

            foreach (var plat in platforms) 
            {
                if(!repo.ExternalPlatformExist(plat.ExternalId)) 
                {
                    repo.CreatePlatform(plat);
                }
                repo.SaveChanges();
            }
        }
    }
}