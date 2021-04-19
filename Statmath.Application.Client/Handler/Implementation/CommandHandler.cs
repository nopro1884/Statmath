using Statmath.Application.Client.Commands.Abstraction;
using Statmath.Application.Client.Handler.Abstraction;
using Statmath.Application.Shared;
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
        private readonly IClearCommand _clearCommand;
        private readonly IDeleteCommand _deleteCommand;

        public CommandHandler(
            IExitCommand exitCommand,
            IReadCommand readCommand,
            IHelpCommand helpCommand,
            ICreateCommand createCommand,
            IClearCommand clearCommand,
            IDeleteCommand deleteCommand)
        {
            // injected command references
            _exitCommand = exitCommand;
            _readCommand = readCommand;
            _helpCommand = helpCommand;
            _createCommand = createCommand;
            _clearCommand = clearCommand;
            _deleteCommand = deleteCommand;

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
                    .Split(Constants.CommandDelimiter);
            }
            catch (Exception)
            {
                // error while splitting userinput
                // return array with size of 1 as result of exception
                userInputFragments = new string[] { string.Empty };
            }

            // check if key exists
            // getting explicit command function for execution
            try
            {
                var possibleCommand = userInputFragments[0];
                if (_commandDict.TryGetValue(possibleCommand, out var command))
                {
                    // collect args
                    var args = userInputFragments.ToList().Skip(1);
                    // initialize command and execute on finish task
                    return await command(args).Result.Execute();
                }
                Console.WriteLine(Constants.UnknownCommand);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return await Task.FromResult(true);
        }

        private void InitCommandDictionary()
        {
            _commandDict.Add(Constants.CommandHelp, _helpCommand.Initialize);
            _commandDict.Add(Constants.CommandExit, _exitCommand.Initialize);
            _commandDict.Add(Constants.CommandRead, _readCommand.Initialize);
            _commandDict.Add(Constants.CommandCreate, _createCommand.Initialize);
            _commandDict.Add(Constants.CommandClear, _clearCommand.Initialize);
            _commandDict.Add(Constants.CommandDelete, _deleteCommand.Initialize);
        }
    }
}
