using Statmath.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Statmath.Application.Repository.Abstraction
{
    public interface IPlanRepository
    {
        Task<int> Add(Plan plan);
        Task<int> Add(List<Plan> plans);
        Plan GetByJob(int job);
        IEnumerable<Plan> GetByMachineName(string machineName);
        IEnumerable<Plan> GetByStartDate(string date);
        IEnumerable<Plan> GetByEndDate(string date);
        IEnumerable<Plan> GetByStartDateTime(string date);
        IEnumerable<Plan> GetByEndDateTime(string date);
        IEnumerable<Plan> GetAll();
    }
}