using Statmath.Application.Task.DataHelper.Implementation;
using System;
using Xunit;

namespace Statmath.Application.Task.DataHelper.Test
{
    public class DateTimeConverterTest
    {
        public DateTimeConverterTest()
        {
            _converter = new DateTimeConverter();
        }

        public DateTimeConverter _converter { get; }

        [Fact]
        public void CompleteDateTimeString()
        {
            var date = _converter.ConvertToDateTime("2020-09-27-19-27");
            Assert.Equal(new DateTime(2020, 09, 27, 19, 27, 0), date);
        }

        [Fact]
        public void IncompleteDateTimeString()
        {
            var date = _converter.ConvertToDateTime("2020-09-27-19-");
            Assert.Equal(DateTime.MaxValue, date);
        }

        [Fact]
        public void CorruptDateTimeString()
        {
            var date = _converter.ConvertToDateTime("2020-09-27-19-");
            Assert.Equal(DateTime.MaxValue, date);
        }

        [Fact]
        public void EmptyDateTimeString()
        {
            var date = _converter.ConvertToDateTime(string.Empty);
            Assert.Equal(DateTime.MaxValue, date);
        }

        [Fact]
        public void NullDateTimeString()
        {
            var date = _converter.ConvertToDateTime(null);
            Assert.Equal(DateTime.MaxValue, date);
        }

        [Fact]
        public void DateTimeToStringValid()
        {
            var dateTimeStr = _converter.ConvertFromDateTime(new DateTime(2020, 09, 27, 19, 27, 0));
            Assert.Equal("2020-09-27-19-27", dateTimeStr);
        }

        [Fact]
        public void DateTimeToStringMaxValue()
        {
            var dateTimeStr = _converter.ConvertFromDateTime(DateTime.MaxValue);
            Assert.Equal("-", dateTimeStr);
        }

        [Fact]
        public void DateTimeToStringFutureValue()
        {
            var dateTimeStr = _converter.ConvertFromDateTime(DateTime.Now.AddDays(1));
            Assert.Equal("-", dateTimeStr);
        }
    }
}
