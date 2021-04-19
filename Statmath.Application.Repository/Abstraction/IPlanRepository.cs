using Statmath.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Statmath.Application.Repository.Abstraction
{
    public interface IPlanRepository
    {
        Task<int> Add(PlanDto plan);
        Task<int> Add(IEnumerable<PlanDto> plans);
        Task<int> Delete(PlanDto plan);
        Task<int> Delete();
        PlanDto GetByJob(int job);
        IEnumerable<PlanDto> GetByMachineName(string machineName);
        IEnumerable<PlanDto> GetByStartDate(string date);
        IEnumerable<PlanDto> GetByEndDate(string date);
        IEnumerable<PlanDto> GetByStartDateTime(string date);
        IEnumerable<PlanDto> GetByEndDateTime(string date);
        IEnumerable<PlanDto> GetAll();
    }
}