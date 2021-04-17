using Statmath.Application.DataHelper.Abstraction;
using System;
using System.Globalization;

namespace Statmath.Application.DataHelper.Implementation
{
    public class DateTimeConverter : IDateTimeConverter
    {
        private const string DateTimeFormat = "yyyy-MM-dd-HH-mm";
        private const string DateTimeInvalid = "-";

        // convert datetime in to csv source time format
        public string ConvertFromDateTime(DateTime date) => date > DateTime.Now
                ? DateTimeInvalid
                : date.ToString(DateTimeFormat);

        // convert csv sourve time format in to datetime
        public DateTime ConvertToDateTime(string date)
        {
            date = date.Length == 10 ? date + "-00-00" : date;
            CultureInfo cultureInfo = new CultureInfo("de-DE");

            if (DateTime.TryParseExact(date, DateTimeFormat, cultureInfo, DateTimeStyles.None, out var dateTime))
                return dateTime;
            else
                return DateTime.MaxValue.Subtract(new TimeSpan(1, 0, 0, 0));
        }
    }
}