using System.Collections.Generic;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    /// <summary>
    /// API Controller for managing commands related to platforms.
    /// </summary>
    [Route("/api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandsController"/> class.
        /// </summary>
        /// <param name="repository">The command repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public CommandsController(ICommandRepo repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandsController"/> class.
        /// </summary>
        /// <param name="repository">The command repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandForPlatform(int platformId) 
        {
            System.Console.WriteLine($"--> Hit GetCommandsForPlatform: {platformId}");

            if(!_repository.PlatformExist(platformId))
            {
                return NotFound();
            }

            var commands = _repository.GetCommandsForPlatform(platformId);
            var commandsDto = _mapper.Map<IEnumerable<CommandReadDto>>(commands);
            return Ok(commandsDto);
        }

        /// <summary>
        /// Gets a specific command for a specific platform.
        /// </summary>
        /// <param name="platformId">The ID of the platform.</param>
        /// <param name="commandId">The ID of the command.</param>
        /// <returns>The command read DTO for the specified command.</returns>
        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId) 
        {
            System.Console.WriteLine($"--> Hit GetCommandForPlatform: {platformId} / {commandId}");

            if(!_repository.PlatformExist(platformId))
            {
                return NotFound();
            }

            var command = _repository.GetCommand(platformId, commandId);

            if(command is null) 
            {
                return NotFound();
            }

            var commandDto = _mapper.Map<CommandReadDto>(command);
            return Ok(commandDto);
        }

        /// <summary>
        /// Creates a new command for a specific platform.
        /// </summary>
        /// <param name="platformId">The ID of the platform.</param>
        /// <param name="commandDto">The command create DTO.</param>
        /// <returns>The created command read DTO.</returns>
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandDto)
        {
            System.Console.WriteLine($"--> Hit CreateCommandForPlatform: {platformId}");

            if(!_repository.PlatformExist(platformId))
            {
                return NotFound();
            }

            var command = _mapper.Map<Command>(commandDto);
            _repository.CreateCommand(platformId, command);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(command);
            
            return CreatedAtRoute(nameof(GetCommandForPlatform), 
                new { platformId = platformId, commandId = commandReadDto.Id }, commandReadDto);
        }
    }
}