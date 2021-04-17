using Statmath.Application.Client.Handler.Abstraction;
using Statmath.Application.DataHelper.Abstraction;
using Statmath.Application.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Statmath.Application.Client.Commands.Abstraction
{
    public class CreateCommand : ICreateCommand
    {
        private readonly IPlanConnectionHandler _connectionHandler;
        private readonly ICsvHelper _csvHelper;
        private List<string> _args;

        public CreateCommand(IPlanConnectionHandler connectionHandler, ICsvHelper csvHelper)
        {
            _connectionHandler = connectionHandler;
            _csvHelper = csvHelper;
        }

        public async Task<bool> Execute()
        {
            // check for existence or at least one argument
            if (_args?.Any() ?? false)
            {
                var filePath = _args.First();
                if (File.Exists(filePath))
                {
                    if (_csvHelper.IsFileNotInUse(filePath))
                    {
                        try
                        {
                            var foo = _csvHelper.ReadCsvFile(filePath).ToList();
                            await _connectionHandler.CreatePlans(foo);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine(Constants.CommandCreateInvalid);
                        }
                    }
                    else
                    {
                        // file already is use
                        Console.WriteLine(Constants.CommandCreateUnreadable);
                    }
                }
                else
                {
                    // file not found
                    Console.WriteLine(Constants.CommandCreateFileNotFound);
                }
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
