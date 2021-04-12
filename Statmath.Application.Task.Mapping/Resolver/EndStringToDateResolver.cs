using AutoMapper;
using Statmath.Application.Task.DataHelper.Abstraction;
using Statmath.Application.Task.Models;
using System;

namespace Statmath.Application.Task.Mapping.Resolver
{
    public class EndStringToDateResolver : IValueResolver<PlanViewModel, Plan, DateTime>
    {
        private readonly IDateTimeConverter _dateTimeConverter;
        public EndStringToDateResolver(IDateTimeConverter dateTimeConverter)
        {
            _dateTimeConverter = dateTimeConverter;
        }

        public DateTime Resolve(PlanViewModel source, Plan destination, DateTime destMember, ResolutionContext context) 
            => _dateTimeConverter.ConvertToDateTime(source.End);
    }
}
