using Statmath.Application.Client.Handler.Abstraction;
using Statmath.Application.DataHelper.Abstraction;
using Statmath.Application.Exceptions;
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
        private readonly IJobConnectionHandler _connectionHandler;
        private readonly ICsvHelper _csvHelper;
        private List<string> _args;

        public CreateCommand(IJobConnectionHandler connectionHandler, ICsvHelper csvHelper)
        {
            _connectionHandler = connectionHandler;
            _csvHelper = csvHelper;
        }

        public async Task<bool> Execute()
        {
            // args not available or empty
            if (_args == default(List<string>) || !_args.Any())
            {
                Console.WriteLine(Constants.CommandCreateNoPath);
                return true;
            }

            // to many arguments
            if (_args.Count() > 1)
            {
                Console.WriteLine(Constants.CommandCreateToManyArgs);
                return true;
            }

            var filePath = _args.First();
            // file not found
            if (!File.Exists(filePath))
            {
                Console.WriteLine(Constants.CommandCreateFileNotFound);
                return true;
            }

            var errMsg = string.Empty;
            try
            {
                // file not in use by another programm
                if (_csvHelper.IsFileNotInUse(filePath))
                {
                    var data = _csvHelper.ReadCsvFile(filePath).ToList();
                    var response = await _connectionHandler.CreateJobs(data);
                    Console.WriteLine(response);
                    return true;
                }
            }
            catch (FileAlreadyInUseException e)
            {
                errMsg = e.Message;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            // print error message to console if any error had thown
            if (!string.IsNullOrWhiteSpace(errMsg))
            {
                Console.WriteLine(errMsg);
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
