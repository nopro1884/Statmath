using Statmath.Application.Task.Models;

namespace Statmath.Application.Task.DataHelper.Abstraction
{
    public interface IPlanConverter
    {
        PlanViewModel ConvertFromCsv(string[] fields);
    }
}