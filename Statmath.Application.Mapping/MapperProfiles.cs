using AutoMapper;
using Statmath.Application.Mapping.Resolver;
using Statmath.Application.Models;

namespace Statmath.Application.Mapping
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            // Map Plan -> PlanViewModel
            CreateMap<PlanDto, PlanViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom<IdToStringResolver>())
                .ForMember(dest => dest.Start, opt => opt.MapFrom<StartDateToStringResolver>())
                .ForMember(dest => dest.End, opt => opt.MapFrom<EndDateToStringResolver>());

            // Map PlanViewModel -> Plan
            CreateMap<PlanViewModel, PlanDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom<IdToGuidResolver>())
                .ForMember(dest => dest.StartedAt, opt => opt.MapFrom<StartStringToDateResolver>())
                .ForMember(dest => dest.EndedAt, opt => opt.MapFrom<EndStringToDateResolver>());

        }
    }
}