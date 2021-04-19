using Statmath.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Statmath.Application.Client.Handler.Abstraction
{
    public interface IPlanConnectionHandler
    {
        Task<int> DeleteAll();
        Task<int> Delete(PlanViewModel viewModel);
        Task<string> CreatePlan(PlanViewModel viewModel);
        Task<string> CreatePlans(IEnumerable<PlanViewModel> viewModels);
        Task<ICollection<PlanViewModel>> GetAll();
        Task<PlanViewModel> GetByJob(int job);
        Task<ICollection<PlanViewModel>> GetByMachine(string machine);
        Task<ICollection<PlanViewModel>> GetByDate(string type, string date);
        Task<ICollection<PlanViewModel>> GetByDateTime(string type, string date);
    }
}
