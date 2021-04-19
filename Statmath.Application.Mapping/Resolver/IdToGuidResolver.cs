using AutoMapper;
using Statmath.Application.Models;
using System;

namespace Statmath.Application.Mapping.Resolver
{
    public class IdToGuidResolver : IValueResolver<PlanViewModel, PlanDto, Guid>
    {
        public Guid Resolve(PlanViewModel source, PlanDto destination, Guid destMember, ResolutionContext context)
        {
            if (Guid.TryParse(source.Id, out var nGuid))
            {
                return nGuid;
            }
            return Guid.NewGuid();
        }
    }
}