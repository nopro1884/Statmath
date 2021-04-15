using Statmath.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Statmath.Application.Client.Handler.Abstraction
{
    public interface IConnectionHandler
    {
        Task CreatePlan(PlanViewModel viewModel);
        Task CreatePlans(IEnumerable<PlanViewModel> viewModels);
    }
}
