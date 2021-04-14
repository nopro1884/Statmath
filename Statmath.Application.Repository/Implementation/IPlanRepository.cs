using Statmath.Application.Models;
using System;
using System.Threading;
using System.Collections.Generic;

namespace Statmath.Application.Repository.Implementation
{
    public interface IPlanRepository
    {
        void Add(Plan plan);

        Plan GetByJob(int job);

        IEnumerable<Plan> GetByMachineName(string machineName);

        IEnumerable<Plan> GetByStartDate(DateTime date);

        IEnumerable<Plan> GetByEndDate(DateTime date);

        IEnumerable<Plan> GetAll();
    }
}