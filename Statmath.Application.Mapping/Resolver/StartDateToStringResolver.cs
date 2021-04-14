﻿using AutoMapper;
using Statmath.Application.DataHelper.Abstraction;
using Statmath.Application.Models;

namespace Statmath.Application.Mapping.Resolver
{
    public class StartDateToStringResolver : IValueResolver<Plan, PlanViewModel, string>
    {
        private readonly IDateTimeConverter _dateTimeConverter;

        public StartDateToStringResolver(IDateTimeConverter dateTimeConverter)
        {
            _dateTimeConverter = dateTimeConverter;
        }

        public string Resolve(Plan source, PlanViewModel destination, string destMember, ResolutionContext context)
            => _dateTimeConverter.ConvertFromDateTime(source.StartedAt);
    }
}