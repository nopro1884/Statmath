using Statmath.Application.Client.Common;
using Statmath.Application.Client.Handler.Abstraction;
using Statmath.Application.DataHelper.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Statmath.Application.Client.Commands.Abstraction
{
    public class CreateCommand : ICreateCommand
    {
        private readonly IConnectionHandler _connectionHandler;
        private readonly ICsvHelper _csvHelper;
        private List<string> _args;

        public CreateCommand(IConnectionHandler connectionHandler, ICsvHelper csvHelper)
        {
            _connectionHandler = connectionHandler;
            _csvHelper = csvHelper;
        }

        public Task<bool> Execute()
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
                            _connectionHandler.CreatePlans(foo);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine(SharedConstants.CommandCreateUnreadable);
                        }
                    }
                    // file already is use
                    Console.WriteLine(SharedConstants.CommandCreateFileNotFound);
                }
                else
                {
                    // file not found
                    Console.WriteLine(SharedConstants.CommandCreateFileNotFound);
                }
            }
            return Task.FromResult(true);
        }

        public virtual Task<ICommand> Initialize(IEnumerable<string> args)
        {
            _args = args.ToList();
            return Task.FromResult<ICommand>(this);
        }
    }
}
