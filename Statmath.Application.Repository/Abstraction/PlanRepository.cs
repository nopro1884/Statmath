using Statmath.Application.Data.Context;
using Statmath.Application.DataHelper.Abstraction;
using Statmath.Application.Models;
using Statmath.Application.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Statmath.Application.Repository.Abstraction
{
    public class PlanRepository : IPlanRepository
    {
        public ApplicationDbContext _context { get; }
        public IDateTimeHelper _dateTimeHelper { get; }

        public PlanRepository(ApplicationDbContext context, IDateTimeHelper dateTimeHelper)
        {
            _context = context;
            _dateTimeHelper = dateTimeHelper;
        }

        public void Add(Plan plan)
        {
            try
            {
                _context.Plans.Add(plan);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Plan> GetAll()
            => _context.Plans.ToList();

        public IEnumerable<Plan> GetByEndDate(DateTime date) =>
            _context.Plans.Where(p => _dateTimeHelper.IsDayEqual(p.EndedAt, date));

        public IEnumerable<Plan> GetByStartDate(DateTime date)
            => _context.Plans.Where(p => _dateTimeHelper.IsDayEqual(p.StartedAt, date));

        public Plan GetByJob(int job)
            => _context.Plans.FirstOrDefault(p => p.Job == job);

        public IEnumerable<Plan> GetByMachineName(string machine)
            => _context.Plans.Where(p => p.Machine == machine);
    }
}