using AutoMapper;
using Statmath.Application.Task.Models;

namespace Statmath.Application.Task.Mapping.Resolver
{
    public class IdToStringResolver : IValueResolver<Plan, PlanViewModel, string>
    {
        public string Resolve(Plan source, PlanViewModel destination, string destMember, ResolutionContext context)
            => source.Id.ToString();
    }
}