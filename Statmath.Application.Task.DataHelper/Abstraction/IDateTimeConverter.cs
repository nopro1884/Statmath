using System;

namespace Statmath.Application.Task.DataHelper.Abstraction
{
    public interface IDateTimeConverter
    {
        string ConvertFromDateTime(DateTime date);
        DateTime ConvertToDateTime(string date);
    }
}
