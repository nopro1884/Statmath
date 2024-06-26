﻿using AutoMapper;
using Statmath.Application.Models;

namespace Statmath.Application.Mapping.Resolver
{
    public class IdToStringResolver : IValueResolver<JobDto, JobViewModel, string>
    {
        public string Resolve(JobDto source, JobViewModel destination, string destMember, ResolutionContext context)
            => source.Id.ToString();
    }
}