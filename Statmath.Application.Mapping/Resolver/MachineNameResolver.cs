using AutoMapper;
using Statmath.Application.Models;

namespace Statmath.Application.Mapping.Resolver
{
    public class MachineNameResolver : IValueResolver<JobDto, JobViewModel, string>
    {
        public string Resolve(JobDto source, JobViewModel destination, string destMember, ResolutionContext context)
        {
            return source.Machine.Name;
        }
    }
}
