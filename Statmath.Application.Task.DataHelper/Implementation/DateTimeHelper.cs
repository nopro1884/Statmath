using Statmath.Application.Task.DataHelper.Abstraction;
using System;

namespace Statmath.Application.Task.DataHelper.Implementation
{
    public class DateTimeHelper : IDateTimeHelper
    {
        public bool IsDayEqual(DateTime date1, DateTime date2)
        {
            date1 = new DateTime(date1.Year, date1.Month, date1.Day);
            date2 = new DateTime(date2.Year, date2.Month, date2.Day);
            return date1.Equals(date2);
        }
    }
}