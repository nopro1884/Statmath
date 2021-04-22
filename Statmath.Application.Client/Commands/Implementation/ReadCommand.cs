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

        /// <summary>
        /// read jobs by date and time
        /// </summary>
        /// <param name="time">time - start or end</param>
        /// <param name="datetime">the date</param>
        /// <returns></returns>
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

        /// <summary>
        /// read jobs by datetime and time
        /// </summary>
        /// <param name="time">time - start or end</param>
        /// <param name="datetime">the date and time</param>
        /// <returns></returns>
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

        /// <summary>
        /// Execution logic
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Execute()
        {
            if (_args?.Any() ?? false)
            {
                // read all
                if (_args.Count() == 1 && _args.First() == Constants.CmdArgAll)
                {
                    // get all stored jobs from database
                    var jobs = await _connectionHandler.GetAll();
                    if (jobs?.Any() ?? false)
                        _printHandler.Print(jobs);
                    else
                        Console.WriteLine("Oups! No jobs found!");
                    return true;
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
                                _printHandler.Print(new List<JobViewModel>() { jobs });
                            else
                                Console.WriteLine($"No jobs with id \"{payload}\"");

                            return true;
                        }
                    }
                    if (_args.First() == Constants.CmdArgMachine)
                    {
                        // get jobs by machine name
                        var jobs = await _connectionHandler.GetByMachine(payload);
                        if (jobs?.Any() ?? false)
                            _printHandler.Print(jobs);
                        else
                            Console.WriteLine($"No jobs for machine \"{payload}\"");

                        return true;
                    }
                }
                // read by multi args command
                if (_args.Count() == 3)
                {
                    var jobs = default(IEnumerable<JobViewModel>);
                    var type = _args[1];
                    payload = _args[2];
                    switch (_args[0])
                    {
                        case Constants.CmdArgDate:
                            jobs = await GetJobsByDate(type, payload);
                            break;
                        case Constants.CmdArgDateTime:
                            jobs = await GetJobsByDateTime(type, payload);
                            break;
                    }
                    if (jobs?.Any() ?? false)
                        _printHandler.Print(jobs);
                    else
                        Console.WriteLine($"No jobs with {type} date  \"{payload}\"");

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
