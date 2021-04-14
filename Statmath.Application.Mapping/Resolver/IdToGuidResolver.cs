using AutoMapper;
using Statmath.Application.Models;
using System;

namespace Statmath.Application.Mapping.Resolver
{
    public class IdToGuidResolver : IValueResolver<PlanViewModel, Plan, Guid>
    {
        public Guid Resolve(PlanViewModel source, Plan destination, Guid destMember, ResolutionContext context)
        {
            if (Guid.TryParse(source.Id, out var nGuid))
            {
                return nGuid;
            }
            return Guid.NewGuid();
        }
    }
}