using Statmath.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Statmath.Application.Client.Handler.Abstraction
{
    public interface IJobConnectionHandler
    {
        Task<int> DeleteAll();
        Task<int> Delete(JobViewModel viewModel);
        Task<string> CreateJob(JobViewModel viewModel);
        Task<string> CreateJobs(IEnumerable<JobViewModel> viewModels);
        Task<ICollection<JobViewModel>> GetAll();
        Task<JobViewModel> GetByJob(int job);
        Task<ICollection<JobViewModel>> GetByMachine(string machine);
        Task<ICollection<JobViewModel>> GetByDate(string type, string date);
        Task<ICollection<JobViewModel>> GetByDateTime(string type, string date);
    }
}
