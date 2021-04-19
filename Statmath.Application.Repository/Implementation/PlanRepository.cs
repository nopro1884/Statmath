using Statmath.Application.Data.Context;
using Statmath.Application.DataHelper.Abstraction;
using Statmath.Application.Models;
using Statmath.Application.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Statmath.Application.Repository.Implementation
{
    public class PlanRepository : IPlanRepository
    {
        public ApplicationDbContext _context { get; }
        public IDateTimeHelper _dateTimeHelper { get; }
        public IDateTimeConverter _dateTimeConverter { get; }

        public PlanRepository(
            ApplicationDbContext context,
            IDateTimeHelper dateTimeHelper,
            IDateTimeConverter dateTimeConverter
            )
        {
            _context = context;
            _dateTimeHelper = dateTimeHelper;
            _dateTimeConverter = dateTimeConverter;
        }

        public async Task<int> Add(PlanDto dto)
        {
            try
            {
                _context.Plans.Add(dto);
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Add(IEnumerable<PlanDto> dtos)
        {
            try
            {
                _context.Plans.AddRange(dtos);
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Delete(PlanDto dto)
        {
            try
            {
                var plan  = _context.Plans.Where(x => x.Id == dto.Id);
                if (plan != default(PlanDto))
                {
                    _context.Plans.Remove(dto);
                    return await _context.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Delete()
        {
            try
            {
                var plans = _context.Plans;
                if (plans?.Any() ?? false)
                {
                    _context.Plans.RemoveRange(plans);
                    return await _context.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<PlanDto> GetAll()
            => _context.Plans.ToList();

        public IEnumerable<PlanDto> GetByEndDate(string date)
        {
            var dateTime = _dateTimeConverter.ConvertToDateTime(date);
            return _context.Plans.ToList().Where(p => _dateTimeHelper.IsDayEqual(p.EndedAt, dateTime));
        }

        public IEnumerable<PlanDto> GetByStartDate(string date)
        {
            var dateTime = _dateTimeConverter.ConvertToDateTime(date);
            return _context.Plans.ToList().Where(p => _dateTimeHelper.IsDayEqual(p.StartedAt, dateTime));
        }

        public IEnumerable<PlanDto> GetByEndDateTime(string date)
        {
            var dateTime = _dateTimeConverter.ConvertToDateTime(date);
            return _context.Plans.Where(p => p.EndedAt.Equals(dateTime));
        }

        public IEnumerable<PlanDto> GetByStartDateTime(string date)
        {
            var dateTime = _dateTimeConverter.ConvertToDateTime(date);
            return _context.Plans.Where(p => p.StartedAt.Equals(dateTime));
        }

        public PlanDto GetByJob(int job)
            => _context.Plans.FirstOrDefault(p => p.Job == job);

        public IEnumerable<PlanDto> GetByMachineName(string machine)
            => _context.Plans.Where(p => p.Machine == machine);


    }
}