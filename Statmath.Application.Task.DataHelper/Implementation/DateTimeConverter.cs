using Statmath.Application.Task.DataHelper.Abstraction;
using System;
using System.Globalization;

namespace Statmath.Application.Task.DataHelper.Implementation
{
    public class DateTimeConverter : IDateTimeConverter
    {
        const string DateTimeFormat = "yyyy-MM-dd-HH-mm";
        const string DateTimeInvalid = "-";

        // convert datetime in to csv source time format
        public string ConvertFromDateTime(DateTime date) => date > DateTime.Now 
                ? DateTimeInvalid 
                : date.ToString(DateTimeFormat);

        // convert csv sourve time format in to datetime 
        public DateTime ConvertToDateTime(string date)
        {
            CultureInfo cultureInfo = new CultureInfo("de-DE");
            
            if (DateTime.TryParseExact(date, DateTimeFormat, cultureInfo, DateTimeStyles.None, out var dateTime))
                return dateTime; 
            else
                return DateTime.MaxValue;
        }
    }
}
