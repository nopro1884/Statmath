using Statmath.Application.Task.Models;
using System;
using System.Collections.Generic;

namespace Statmath.Application.Task.Repository.Implementation
{
    public interface IPlanRepository
    {
        void Add(Plan plan);
        void Add(IEnumerable<Plan> planCollection);
        Plan GetById(Guid id);
        Plan GetByJob(int job);
        IEnumerable<Plan> GetByMachineName(string machineName);
        IEnumerable<Plan> GetByStartDate(DateTime date);
        IEnumerable<Plan> GetByEndDate(DateTime date);
        IEnumerable<Plan> GetAll();
    }
}
