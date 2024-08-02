using System.Collections.Generic;
using CommandsService.Models;

namespace CommandsService.SyncDataService.Grpc 
{
    /// <summary>
    /// Provides methods for interacting with platform data.
    /// </summary>
    public interface IPlatformDataClient 
    {
        IEnumerable<Platform> ReturnAllPlatforms();
    }
}