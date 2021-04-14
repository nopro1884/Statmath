using System;

namespace Statmath.Application.Task.DataHelper.Abstraction
{
    public interface IDateTimeHelper
    {
        bool IsDayEqual(DateTime date1, DateTime date2);
    }
}