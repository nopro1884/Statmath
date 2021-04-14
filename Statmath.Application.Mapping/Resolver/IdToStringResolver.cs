using AutoMapper;
using Statmath.Application.Models;

namespace Statmath.Application.Mapping.Resolver
{
    public class IdToStringResolver : IValueResolver<Plan, PlanViewModel, string>
    {
        public string Resolve(Plan source, PlanViewModel destination, string destMember, ResolutionContext context)
            => source.Id.ToString();
    }
}