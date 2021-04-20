using AutoMapper;
using Statmath.Application.Models;

namespace Statmath.Application.Mapping.Resolver
{
    public class MachineResolver : IValueResolver<JobViewModel, JobDto, MachineDto>
    {
        public MachineDto Resolve(JobViewModel source, JobDto destination, MachineDto destMember, ResolutionContext context)
        {
            return new MachineDto { Name = source.Machine };
        }
    }
}
