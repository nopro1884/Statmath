using Statmath.Application.Client.Handler.Abstraction;
using Statmath.Application.Models;
using Statmath.Application.Shared;
using System;
using System.Collections.Generic;

namespace Statmath.Application.Client.Handler.Implementation
{
    public class PrintHandler : IPrintHandler
    {
        private IEnumerable<PlanViewModel> _viewModels = default(IEnumerable<PlanViewModel>);

        public ICollection<PlanViewModel> ViewModels => throw new NotImplementedException();

        public void Print(IEnumerable<PlanViewModel> viewModels)
        {
            _viewModels = viewModels;

            PrintHeader();
            foreach (var vm in viewModels)
            {
                PrintRow(vm);
            }
            PrintFooter();
        }

        private void PrintHeader()
        {
            Console.WriteLine(Constants.HeaderHorizontalLine);
            Console.WriteLine(Constants.HeaderRow);
            Console.WriteLine(Constants.HeaderHorizontalLine);
        }

        private void PrintFooter()
        {
            Console.WriteLine(Constants.HeaderHorizontalLine);
        }

        private void PrintRow(PlanViewModel vm)
        {
            var textMachine = vm.Machine.PadLeft(Constants.HeaderMachine.Length-1);
            var textJob = Convert.ToString(vm.Job).PadLeft(Constants.HeaderJob.Length-1);
            var textStartDate = vm.Start.PadLeft(Constants.HeaderStartDate.Length-1);
            var textEndDate = vm.End.PadLeft(Constants.HeaderEndDate.Length-1);

            var row = @$"|{textMachine} |{textJob} |{textStartDate} |{textEndDate} |";
            Console.WriteLine(row);
        }
    }
}
