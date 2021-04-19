using AutoMapper;
using Statmath.Application.Models;

namespace Statmath.Application.Mapping.Resolver
{
    public class IdToStringResolver : IValueResolver<PlanDto, PlanViewModel, string>
    {
        public string Resolve(PlanDto source, PlanViewModel destination, string destMember, ResolutionContext context)
            => source.Id.ToString();
    }
}