using Statmath.Application.Client.Handler.Abstraction;
using Statmath.Application.Shared;
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
                Console.Write(Constants.ConsolePrefix);
                // catch input from user and handle possible commands 
                var userInput = Console.ReadLine();
                isActive = await _commandHandler.HandleCommand(userInput);
                // let it breath
                Thread.Sleep(25);
            } while (isActive);
            await Task.CompletedTask;
        }
    }
}
