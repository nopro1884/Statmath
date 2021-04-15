using Statmath.Application.Client.Commands.Abstraction;
using Statmath.Application.Client.Common;
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
                SharedConstants.HorizontalLine,
                $"{SharedConstants.CommandStore}\t\t\t--> store data from csv file into database",
                $"{SharedConstants.CommandRead}\t\t\t--> read data from database",
                $"{SharedConstants.CommandExit}\t\t\t--> exit application",
                SharedConstants.HorizontalLine,
            };
            listing.ForEach(Console.WriteLine);
            return Task.FromResult(true);
        }
    }
}
