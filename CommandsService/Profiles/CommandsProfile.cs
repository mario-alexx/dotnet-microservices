using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;
using PlatformService;

namespace CommandsService.Profiles 
{
    /// <summary>
    /// Defines AutoMapper profiles for mapping between command and platform entities and their respective DTOs.
    /// </summary>
    public class CommandsProfile : Profile   
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandsProfile"/> class.
        /// </summary>
        public CommandsProfile()
        {
            // Source -> Target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<Command, CommandReadDto>();
            CreateMap<PlatformPublishedDto, Platform>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));
            CreateMap<GrpcPlatformModel, Platform>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src .PlatformId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src .Name))
                .ForMember(dest => dest.Commands, opt => opt.Ignore());
        }
    }
}