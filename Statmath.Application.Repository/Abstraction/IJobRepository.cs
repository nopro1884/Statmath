using Statmath.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Statmath.Application.Repository.Abstraction
{
    public interface IJobRepository
    {
        Task<int> Add(JobDto job);
        Task<int> Add(IEnumerable<JobDto> jobs);
        Task<int> Delete(JobDto job);
        Task<int> Delete();
        JobDto GetByJob(int jobId);
        IEnumerable<JobDto> GetByMachineName(string machineName);
        IEnumerable<JobDto> GetByStartDate(string date);
        IEnumerable<JobDto> GetByEndDate(string date);
        IEnumerable<JobDto> GetByStartDateTime(string date);
        IEnumerable<JobDto> GetByEndDateTime(string date);
        IEnumerable<JobDto> GetAll();
    }
}