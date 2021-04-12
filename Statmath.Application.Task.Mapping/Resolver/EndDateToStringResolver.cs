using AutoMapper;
using Statmath.Application.Task.DataHelper.Abstraction;
using Statmath.Application.Task.Models;

namespace Statmath.Application.Task.Mapping.Resolver
{
    public class EndDateToStringResolver : IValueResolver<Plan, PlanViewModel, string>
    {
        private readonly IDateTimeConverter _dateTimeConverter;
        public EndDateToStringResolver(IDateTimeConverter dateTimeConverter)
        {
            _dateTimeConverter = dateTimeConverter;
        }

        public string Resolve(Plan source, PlanViewModel destination, string destMember, ResolutionContext context)
            => _dateTimeConverter.ConvertFromDateTime(source.EndedAt);
    }
}
