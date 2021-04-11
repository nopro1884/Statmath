using Statmath.Application.Task.DataHelper.Implementation;
using System;
using Xunit;

namespace Statmath.Application.Task.DataHelper.Test
{
    public class DateTimeHelperTest
    {

        public DateTimeHelperTest()
        {
            _dateTimeHelper = new DateTimeHelper();
        }

        public DateTimeHelper _dateTimeHelper { get; }

        [Fact]
        public void DateTimeIsNotEqual()
        {
            var date1 = new DateTime(1984, 08, 18);
            var date2 = DateTime.Now;
            Assert.False(_dateTimeHelper.IsDayEqual(date1, date2));
        }

        [Fact]
        public void DateTimeIsEqual()
        {
            var date1 = new DateTime(2018,03,21);
            var date2 = new DateTime(2018,03,21);
            Assert.True(_dateTimeHelper.IsDayEqual(date1, date2));
        }

        [Fact]
        public void DateTimeIsEqualWithoutHoursMinutes()
        {
            var date1 = new DateTime(2020,07,01,18,12,2);
            var date2 = new DateTime(2020,07,01,16,10,2);
            Assert.True(_dateTimeHelper.IsDayEqual(date1, date2));
        }
    }
}
