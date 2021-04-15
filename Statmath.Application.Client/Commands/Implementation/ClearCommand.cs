using Statmath.Application.Client.Commands.Abstraction;
using System.Threading.Tasks;

namespace Statmath.Application.Client.Commands.Implementation
{
    public class ClearCommand : IClearCommand
    {
        public Task<bool> Execute()
        {
            System.Console.Clear();
            return Task.FromResult(true);
        }
    }
}
