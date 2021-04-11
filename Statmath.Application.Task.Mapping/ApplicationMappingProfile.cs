using AutoMapper;
using Statmath.Application.Task.Models;

namespace Statmath.Application.Task.Mapping
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<Plan, PlanViewModel>();
        }
    }
}
