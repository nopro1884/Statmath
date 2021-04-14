using Statmath.Application.Models;

namespace Statmath.Application.DataHelper.Abstraction
{
    public interface IPlanConverter
    {
        PlanViewModel ConvertFromCsv(string[] fields);
    }
}