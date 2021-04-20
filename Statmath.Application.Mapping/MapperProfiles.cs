using AutoMapper;
using Statmath.Application.Mapping.Resolver;
using Statmath.Application.Models;

namespace Statmath.Application.Mapping
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            // Map JobDto -> JobViewModel
            CreateMap<JobDto, JobViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom<IdToStringResolver>())
                .ForMember(dest => dest.Machine, opt => opt.MapFrom<MachineNameResolver>())
                .ForMember(dest => dest.Start, opt => opt.MapFrom<StartDateToStringResolver>())
                .ForMember(dest => dest.End, opt => opt.MapFrom<EndDateToStringResolver>());

            // Map JobViewModel -> JobDto
            CreateMap<JobViewModel, JobDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom<IdToGuidResolver>())
                .ForMember(dest => dest.Machine, opt => opt.MapFrom<MachineResolver>())
                .ForMember(dest => dest.StartedAt, opt => opt.MapFrom<StartStringToDateResolver>())
                .ForMember(dest => dest.EndedAt, opt => opt.MapFrom<EndStringToDateResolver>());


        }
    }
}