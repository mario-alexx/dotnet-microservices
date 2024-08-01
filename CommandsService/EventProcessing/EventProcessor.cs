using System;
using System.Text.Json;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CommandsService.EventProcessing
{
    /// <summary>
    /// Processes events received from the message bus.
    /// </summary>
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventProcessor"/> class.
        /// </summary>
        /// <param name="scopeFactory">The service scope factory for creating service scopes.</param>
        /// <param name="mapper">The AutoMapper instance for mapping objects.</param>
        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper =mapper;
        }

        /// <inheritdoc />
        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType) 
            {
                case EventType.PlatformPusblished:
                    // TO DO
                    break;
                default: 
                    break;
            }
        }

        /// <summary>
        /// Determines the type of event based on the notification message.
        /// </summary>
        /// <param name="notificationMessage">The notification message to analyze.</param>
        /// <returns>The determined event type.</returns>
        private EventType DetermineEvent(string notificationMessage) 
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            switch(eventType.Event)
            {
                case "Platform Published":
                    Console.WriteLine("--> Platform Published Event Detected");
                    return EventType.PlatformPusblished;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

         /// <summary>
        /// Adds a platform based on the published platform message.
        /// </summary>
        /// <param name="platformPublishedMessage">The platform published message.</param>
        private void addPlatform(string PlatformPusblishedMessage) 
        {
            using (var scope = _scopeFactory.CreateScope()) 
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();

                var platformPusblishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(PlatformPusblishedMessage);
               
                try 
                {
                    var plat = _mapper.Map<Platform>(platformPusblishedDto);
                    if(!repo.ExternalPlatformExist(plat.ExternalId)) 
                    {
                        repo.CreatePlatform(plat);
                        repo.SaveChanges();
                    }
                    else 
                    {
                        Console.WriteLine("--> Platform already exist...");
                    }
                }
                catch (Exception ex) 
                {
                    Console.WriteLine($"--> Could not add Platform to DB { ex.Message }");  
                }
            }
        }
    }

    /// <summary>
    /// Defines the types of events that can be processed.
    /// </summary>
    enum EventType 
    {
        /// <summary>
        /// Indicates that a platform was published.
        /// </summary>
        PlatformPusblished,
         /// <summary>
        /// Indicates an undetermined event type.
        /// </summary>
        Undetermined
    }
}