using System;

namespace Statmath.Application.DataHelper.Abstraction
{
    public interface IDateTimeConverter
    {
        string ConvertFromDateTime(DateTime date);

        DateTime ConvertToDateTime(string date);
    }
}