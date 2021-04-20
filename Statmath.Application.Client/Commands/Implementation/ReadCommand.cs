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
    public class ReadCommand : IReadCommand
    {
        private readonly IJobConnectionHandler _connectionHandler;
        private readonly IPrintHandler _printHandler;
        private List<string> _args;

        public ReadCommand(IJobConnectionHandler connectionHandler, IPrintHandler printHandler)
        {
            _connectionHandler = connectionHandler;
            _printHandler = printHandler;
        }

        private async Task<IEnumerable<JobViewModel>> GetJobsByDate(string time, string date)
        {
            switch (time)
            {
                case Constants.CmdArgDateStart:
                    return await _connectionHandler.GetByDate(Constants.CmdArgDateStart, date);
                case Constants.CmdArgDateEnd:
                    return await _connectionHandler.GetByDate(Constants.CmdArgDateEnd, date);
                default:
                    Console.WriteLine(Constants.CommandReadInvalidCommand);
                    return null;
            }
        }

        private async Task<IEnumerable<JobViewModel>> GetJobsByDateTime(string time, string datetime)
        {
            switch (time)
            {
                case Constants.CmdArgDateStart:
                    return await _connectionHandler.GetByDateTime(Constants.CmdArgDateStart, datetime);
                case Constants.CmdArgDateEnd:
                    return await _connectionHandler.GetByDateTime(Constants.CmdArgDateEnd, datetime);
                default:
                    Console.WriteLine(Constants.CommandReadInvalidCommand);
                    return null;
            }
        }

        public async Task<bool> Execute()
        {
            if (_args?.Any() ?? false)
            {
                var results = default(IEnumerable<JobViewModel>);
                // parse combinations
                // read all
                if (_args.Count() == 1 && _args.First() == Constants.CmdArgAll)
                {
                    // get all stored jobs from database
                    // todo: handle output
                    var jobs = await _connectionHandler.GetAll();
                    if (jobs?.Any() ?? false)
                    {
                        results = jobs;
                    }
                }
                // read by uni arg command
                var payload = _args.Last();
                if (_args.Count() == 2)
                {
                    if (_args.First() == Constants.CmdArgJob)
                    {
                        if (int.TryParse(payload, out var job))
                        {
                            // get job by job id
                            var jobs = await _connectionHandler.GetByJob(job);
                            if (jobs != default(JobViewModel))
                            {
                                results = new List<JobViewModel>() { jobs };
                            }
                        }
                    }
                    if (_args.First() == Constants.CmdArgMachine)
                    {
                        // get jobs by machine name
                        var jobs = await _connectionHandler.GetByMachine(payload);
                        if (jobs?.Any() ?? false)
                        {
                            results = jobs;
                        }
                    }
                }
                // read by multi args command
                if (_args.Count() == 3)
                {
                    switch (_args[0])
                    {
                        case Constants.CmdArgDate:
                            var jobs = await GetJobsByDate(_args[1], _args[2]);
                            if (jobs?.Any() ?? false)
                            {
                                results = jobs;
                            }
                            break;
                        case Constants.CmdArgDateTime:
                            jobs = await GetJobsByDateTime(_args[1], _args[2]);
                            if (jobs?.Any() ?? false)
                            {
                                results = jobs;
                            }
                            break;
                    }
                }
                if (results?.Any() ?? false)
                {
                    _printHandler.Print(results);
                    return true;
                }
            }
            Console.WriteLine(Constants.CommandReadInvalidCommand);
            return true;
        }

        public virtual Task<ICommand> Initialize(IEnumerable<string> args)
        {
            _args = args.ToList();
            return Task.FromResult<ICommand>(this);
        }
    }
}
