using Statmath.Application.Client.Commands.Abstraction;
using Statmath.Application.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Statmath.Application.Client.Commands.Implementation
{
    public class HelpCommand : IHelpCommand
    {
        Task<bool> ICommand.Execute()
        {
            var listing = new List<string>
            {
                Constants.HorizontalLine,
                $"{Constants.CommandCreate}\t\t\t--> store data from csv file into database",
                $"{Constants.CommandRead}\t\t\t--> read data from database",
                $"{Constants.CommandDelete}\t\t\t--> delete entries",
                $"{Constants.CommandExit}\t\t\t--> exit application",
                Constants.HorizontalLine,
            };
            listing.ForEach(Console.WriteLine);
            return Task.FromResult(true);
        }
    }
}
