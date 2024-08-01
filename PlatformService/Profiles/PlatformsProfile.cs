using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles 
{
    /// <summary>
    /// Defines AutoMapper profiles for mapping between platform entities and their respective DTOs.
    /// </summary>
    public class PlatformsProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformsProfile"/> class.
        /// </summary>
        public PlatformsProfile()
        {
            // Source -> Target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();
            CreateMap<PlatformReadDto, PlatformPublishedDto>();
        }
    }
}