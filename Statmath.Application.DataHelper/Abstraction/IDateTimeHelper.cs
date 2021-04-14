using System;

namespace Statmath.Application.DataHelper.Abstraction
{
    public interface IDateTimeHelper
    {
        bool IsDayEqual(DateTime date1, DateTime date2);
    }
}