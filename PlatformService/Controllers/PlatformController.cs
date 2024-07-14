using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers 
{   
    /// <summary>
    /// Controller for managing platforms.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformController"/> class.
        /// </summary>
        /// <param name="repository">The repository for platform data.</param>
        /// <param name="mapper">The mapper for DTO conversions.</param>
        public PlatformController(IPlatformRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto) 
        {
            Platform platformModel = _mapper.Map<Platform>(platformCreateDto);
            
            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();
            
            var platformReadDto =  _mapper.Map<PlatformReadDto>(platformModel);
            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
        }
    }
}