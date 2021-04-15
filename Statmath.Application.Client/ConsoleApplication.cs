using Statmath.Application.Client.Common;
using Statmath.Application.Client.Handler.Abstraction;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Statmath.Application.Client
{
    public class ConsoleApplication
    {
        private readonly ICommandHandler _commandHandler;

        public ConsoleApplication(ICommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        public async Task RunAsync()
        {
            bool isActive;
            do
            {
                // print prefix in each line
                Console.Write(SharedConstants.ConsolePrefix);
                // catch input from user and handle possible commands 
                var userInput = Console.ReadLine();
                isActive = await _commandHandler.HandleCommand(userInput);
                // give cpu away
                Thread.Sleep(25);
            } while (isActive);
            await Task.CompletedTask;
        }
    }
}
