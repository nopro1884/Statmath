using System.Threading.Tasks;

namespace Statmath.Application.Client.Handler.Abstraction
{
    public interface ICommandHandler
    {
        public Task<bool> HandleCommand(string userInput);
    }
}
