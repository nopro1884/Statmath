using Statmath.Application.Client.Commands.Abstraction;
using System.Threading.Tasks;

namespace Statmath.Application.Client.Commands.Implementation
{
    public class ExitCommand : IExitCommand
    {
        public Task<bool> Execute()
        {
            // return false to exit the programm
            return Task.FromResult(false);
        }
    }
}
