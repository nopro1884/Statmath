using Statmath.Application.Models;
using System.Collections.Generic;

namespace Statmath.Application.DataHelper.Abstraction
{
    public interface ICsvHelper
    {
        public bool IsFileNotInUse(string filePath);
        IEnumerable<PlanViewModel> ReadCsvFile(string filePath, bool isHeaderIncluded = false);

    }
}
