using Statmath.Application.Client.Commands.Abstraction;
using Statmath.Application.Client.Common;
using Statmath.Application.Client.Handler.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Statmath.Application.Client.Handler.Implementation
{
    public class CommandHandler : ICommandHandler
    {
        private readonly IDictionary<string, Func<IEnumerable<string>, Task<ICommand>>> _commandDict;
        private readonly IExitCommand _exitCommand;
        private readonly IReadCommand _readCommand;
        private readonly IHelpCommand _helpCommand;
        private readonly ICreateCommand _createCommand;


        public CommandHandler(
            IExitCommand exitCommand,
            IReadCommand readCommand, 
            IHelpCommand helpCommand, 
            ICreateCommand createCommand)
        {
            // injected command references
            _exitCommand = exitCommand;
            _readCommand = readCommand;
            _helpCommand = helpCommand;
            _createCommand = createCommand;

            // initialize command dictionary
            _commandDict = new Dictionary<string, Func<IEnumerable<string>, Task<ICommand>>>();
            InitCommandDictionary();
        }

        public async Task<bool> HandleCommand(string userInput)
        {
            string[] userInputFragments;
            try
            {
                // try splitting input into command and possible parameters
                userInputFragments = userInput
                    .TrimStart()
                    .TrimEnd()
                    .Split(SharedConstants.CommandDelimiter);
            }
            catch (Exception)
            {
                // error while splitting userinput
                // return array with size of 1 as result of exception
                userInputFragments = new string[] { string.Empty };
            }

            // 
            var possibleCommand = userInputFragments[0];
            if (_commandDict.ContainsKey(possibleCommand))
            {
                if (_commandDict.TryGetValue(possibleCommand, out var callback))
                {
                    // collect args
                    var args = userInputFragments.Length > 2
                        ? userInputFragments.ToList().Skip(1)
                        : null;
                    // initialize command and execute on finish task
                    return await callback(args).Result.Execute();
                }
            }
            return await HandleUnknownCommand();
        }

        private void InitCommandDictionary()
        {
            _commandDict.Add(SharedConstants.CommandHelp, _helpCommand.Initialize);
            _commandDict.Add(SharedConstants.CommandExit, _exitCommand.Initialize);
        }


        private Task<bool> HandleUnknownCommand()
        {
            Console.WriteLine("Unkown command. For more information enter --help");
            return Task.FromResult(true);
        }



        private bool HandleExitCommand(IEnumerable<string> args)
        {
            return false;
        }
    }
}
