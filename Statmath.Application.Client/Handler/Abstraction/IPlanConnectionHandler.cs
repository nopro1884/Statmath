using Statmath.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Statmath.Application.Client.Handler.Abstraction
{
    public interface IPlanConnectionHandler
    {
        Task CreatePlan(PlanViewModel viewModel);
        Task CreatePlans(IEnumerable<PlanViewModel> viewModels);
        Task<ICollection<PlanViewModel>> GetAll();
        Task<PlanViewModel> GetByJob(int job);
        Task<ICollection<PlanViewModel>> GetByMachine(string machine);
        Task<ICollection<PlanViewModel>> GetByDate(string type, string date);
        Task<ICollection<PlanViewModel>> GetByDateTime(string type, string date);
    }
}
