using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers 
{
    /// <summary>
    /// Controller for managing platform-related actions.
    /// </summary>
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase 
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformsController"/> class.
        /// </summary>
        public PlatformsController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms() 
        {
            Console.WriteLine("--> Getting Platform from CommandsService");

            var platformItems = _repository.GetAllPlatforms();
            var platformDto = _mapper.Map<IEnumerable<PlatformReadDto>>(platformItems);
            
            return Ok(platformDto);
        }

        /// <summary>
        /// Tests the inbound connection to the API.
        /// </summary>
        /// <returns>An <see cref="ActionResult"/> indicating the result of the operation.</returns>
        [HttpPost]
        public ActionResult TestInboundConnection() 
        {
            Console.WriteLine("--> Inbound POST # Command Service");

            return Ok("Inbound test of from Platforms Controller");
        }
    }
    
}