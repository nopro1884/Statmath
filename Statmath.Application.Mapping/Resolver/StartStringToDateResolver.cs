﻿using AutoMapper;
using Statmath.Application.DataHelper.Abstraction;
using Statmath.Application.Models;
using System;

namespace Statmath.Application.Mapping.Resolver
{
    public class StartStringToDateResolver : IValueResolver<PlanViewModel, Plan, DateTime>
    {
        private readonly IDateTimeConverter _dateTimeConverter;

        public StartStringToDateResolver(IDateTimeConverter dateTimeConverter)
        {
            _dateTimeConverter = dateTimeConverter;
        }

        public DateTime Resolve(PlanViewModel source, Plan destination, DateTime destMember, ResolutionContext context)
            => _dateTimeConverter.ConvertToDateTime(source.Start);
    }
}