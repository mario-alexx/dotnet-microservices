using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsynDataServices;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers 
{   
    /// <summary>
    /// Controller for managing platforms.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformController"/> class.
        /// </summary>
        /// <param name="repository">The repository for platform data.</param>
        /// <param name="mapper">The mapper for DTO conversions.</param>
        /// <param name="commandDataClient">The command data client interface.</param>
        /// <param name="messageBusClient">The message bus client interface</param>
        public PlatformsController(
            IPlatformRepo repository, 
            IMapper mapper,
            ICommandDataClient commandDataClient,
            IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
        }

         /// <summary>
        /// Gets a list of all platforms.
        /// </summary>
        /// <returns>A list of platforms.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms() 
        {
            Console.WriteLine("--> Getting Platforms...");
            
            IEnumerable<Platform> platformItems = _repository.GetAllPlatform();
            var platformsReadDto = _mapper.Map<IEnumerable<PlatformReadDto>>(platformItems);
            
            return Ok(platformsReadDto);
        }

         /// <summary>
        /// Gets a platform by its ID.
        /// </summary>
        /// <param name="id">The ID of the platform.</param>
        /// <returns>The platform with the specified ID.</returns>
        [HttpGet("{id:int}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id) 
        {
            Platform platformItem = _repository.GetPlatformById(id);

            if(platformItem is null) 
                return NotFound();
            
            var platformReadDto = _mapper.Map<PlatformReadDto>(platformItem);
            return Ok(platformReadDto);
        }

        /// <summary>
        /// Creates a new platform.
        /// </summary>
        /// <param name="platformCreateDto">The DTO for creating a new platform.</param>
        /// <returns>The created platform.</returns>
        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto) 
        {
            Platform platformModel = _mapper.Map<Platform>(platformCreateDto);
            
            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();
            
            var platformReadDto =  _mapper.Map<PlatformReadDto>(platformModel);

            // Send Sync Message
            try 
            {
                await _commandDataClient.SendPlatformToCommand(platformReadDto);
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"--> Could not send synchronously: { ex.Message }");
            }

            // Send Async Message
            try 
            {
                var platformPublishedDto = _mapper.Map<PlatformPublishedDto>(platformReadDto);
                platformPublishedDto.Event = "Platform_Published";
                _messageBusClient.PublishNewPlatform(platformPublishedDto);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"--> Could not send asynchronously: { ex.Message }");
            }
           
            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
        }
    }
}