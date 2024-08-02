using System;
using System.Collections.Generic;
using AutoMapper;
using CommandsService.Models;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using PlatformService;

namespace CommandsService.SyncDataService.Grpc 
{
    /// <summary>
    /// Client for interacting with platform data.
    /// </summary>
    public class PlatformDataClient : IPlatformDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformDataClient"/> class.
        /// </summary>
        /// <param name="configuration">The configuration settings.</param>
        /// <param name="mapper">The AutoMapper instance for mapping objects.</param>
        public PlatformDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all platforms.
        /// </summary>
        /// <returns>A collection of all platforms.</returns>
        public IEnumerable<Platform> ReturnAllPlatforms()
        {
            Console.WriteLine($"--> Calling Grpc Services {_configuration["GrpcPlatform"]}");

            var channel = GrpcChannel.ForAddress(_configuration["GrpcPlatform"]);
            var client = new GrpcPlatform.GrpcPlatformClient(channel);
            var request = new GetAllRequest();

            try 
            {
                var reply = client.GetAllPlatforms(request);
                return _mapper.Map<IEnumerable<Platform>>(reply.Platform);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"--> Couldnot call GRP Server {ex.Message}");
                return null;
            }
        }
    }
}