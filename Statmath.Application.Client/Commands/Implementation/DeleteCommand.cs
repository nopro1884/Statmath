using Statmath.Application.Client.Commands.Abstraction;
using Statmath.Application.Client.Handler.Abstraction;
using Statmath.Application.Models;
using Statmath.Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Statmath.Application.Client.Commands.Implementation
{
    public class DeleteCommand : IDeleteCommand
    {
        private readonly IPlanConnectionHandler _connectionHandler;
        private ICollection<string> _args;

        public DeleteCommand(IPlanConnectionHandler connectionHandler)
        {
            _connectionHandler = connectionHandler;
        }

        public async Task<bool> Execute()
        {
            int affectedRows = 0;

            try
            {

                if (_args.Count() == 1)
                {
                    var firstArg = _args.First();
                    if (firstArg == Constants.CmdArgAll)
                    {
                        // delete all entries from plan table
                        affectedRows = await _connectionHandler.DeleteAll();
                    }
                    else
                    {
                        Console.WriteLine(Constants.UnknownCommandWrongArg);
                    }
                }
                else if (_args.Count() == 2)
                {
                    // get first arg
                    var firstArg = _args.First();
                    if (firstArg == Constants.CmdArgJob)
                    {
                        // delete single entry by job id
                        var secondArg = _args.Last();
                        if (int.TryParse(secondArg, out var job))
                        {
                            // getting job that should deleted
                            var vm = await _connectionHandler.GetByJob(job);
                            if (vm == default(PlanViewModel))
                            {
                                // job not found -> no delete execution
                                Console.WriteLine($"Unable to find plan with job {job}");
                            }
                            else
                            {
                                // delete
                                affectedRows = await _connectionHandler.Delete(vm);
                            }
                        }
                        else
                        {
                            // convert fail
                            throw new Exception($"Unable to convert {secondArg} to {nameof(Int32)}");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                var message = affectedRows == 0
                     ? "No plans deleted"
                     : $"{affectedRows} deleted";
                Console.WriteLine(message);
            }
            return true;
        }

        public virtual Task<ICommand> Initialize(IEnumerable<string> args)
        {
            _args = args.ToList();
            return Task.FromResult<ICommand>(this);
        }
    }
}
