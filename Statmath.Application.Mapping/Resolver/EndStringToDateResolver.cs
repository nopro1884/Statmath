using AutoMapper;
using Statmath.Application.DataHelper.Abstraction;
using Statmath.Application.Models;
using System;

namespace Statmath.Application.Mapping.Resolver
{
    public class EndStringToDateResolver : IValueResolver<JobViewModel, JobDto, DateTime>
    {
        private readonly IDateTimeConverter _dateTimeConverter;

        public EndStringToDateResolver(IDateTimeConverter dateTimeConverter)
        {
            _dateTimeConverter = dateTimeConverter;
        }

        public DateTime Resolve(JobViewModel source, JobDto destination, DateTime destMember, ResolutionContext context)
            => _dateTimeConverter.ConvertToDateTime(source.End);
    }
}