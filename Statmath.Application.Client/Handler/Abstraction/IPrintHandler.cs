using Statmath.Application.Models;
using System.Collections.Generic;

namespace Statmath.Application.Client.Handler.Abstraction
{
    public interface IPrintHandler
    {
        void Print(IEnumerable<JobViewModel> viewModels);
    }
}
