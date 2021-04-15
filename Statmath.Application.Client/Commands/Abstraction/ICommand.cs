using System.Collections.Generic;
using System.Threading.Tasks;

namespace Statmath.Application.Client.Commands.Abstraction
{
    public interface ICommand
    {
        virtual Task<ICommand> Initialize(IEnumerable<string> args) { return Task.FromResult(this); }
        Task<bool> Execute();
    }
}
