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

        public async Task<int> Add(Plan plan)
        {
            try
            {
                _context.Plans.Add(plan);
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Add(List<Plan> plans)
        {
            try
            {
                _context.Plans.AddRange(plans);
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Plan> GetAll()
            => _context.Plans.ToList();

        public IEnumerable<Plan> GetByEndDate(string date)
        {
            var dateTime = _dateTimeConverter.ConvertToDateTime(date);
            return _context.Plans.ToList().Where(p => _dateTimeHelper.IsDayEqual(p.EndedAt, dateTime));
        }

        public IEnumerable<Plan> GetByStartDate(string date)
        {
            var dateTime = _dateTimeConverter.ConvertToDateTime(date);
            return _context.Plans.ToList().Where(p => _dateTimeHelper.IsDayEqual(p.StartedAt, dateTime));
        }

        public IEnumerable<Plan> GetByEndDateTime(string date)
        {
            var dateTime = _dateTimeConverter.ConvertToDateTime(date);
            return _context.Plans.Where(p => p.EndedAt.Equals(dateTime));
        }

        public IEnumerable<Plan> GetByStartDateTime(string date)
        {
            var dateTime = _dateTimeConverter.ConvertToDateTime(date);
            return _context.Plans.Where(p => p.StartedAt.Equals(dateTime));
        }

        public Plan GetByJob(int job)
            => _context.Plans.FirstOrDefault(p => p.Job == job);

        public IEnumerable<Plan> GetByMachineName(string machine)
            => _context.Plans.Where(p => p.Machine == machine);
    }
}