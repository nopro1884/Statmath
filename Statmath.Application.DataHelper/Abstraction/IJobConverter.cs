using Statmath.Application.Models;

namespace Statmath.Application.DataHelper.Abstraction
{
    public interface IJobConverter
    {
        JobViewModel ConvertFromCsv(string[] fields);
    }
}