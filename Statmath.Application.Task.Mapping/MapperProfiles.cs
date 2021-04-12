using AutoMapper;
using Statmath.Application.Task.Mapping.Resolver;
using Statmath.Application.Task.Models;

namespace Statmath.Application.Task.Mapping
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            // Map Plan -> PlanViewModel
            CreateMap<Plan, PlanViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom<IdToStringResolver>())
                .ForMember(dest => dest.Start, opt => opt.MapFrom<StartDateToStringResolver>())
                .ForMember(dest => dest.End, opt => opt.MapFrom<EndDateToStringResolver>());

            // Map PlanViewModel -> Plan
            CreateMap<PlanViewModel, Plan>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom<IdToGuidResolver>())
                .ForMember(dest => dest.StartedAt, opt => opt.MapFrom<StartStringToDateResolver>())
                .ForMember(dest => dest.EndedAt, opt => opt.MapFrom<EndStringToDateResolver>());
        }
    }
}
