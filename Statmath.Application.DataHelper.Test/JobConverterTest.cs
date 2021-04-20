using Statmath.Application.DataHelper.Implementation;
using Xunit;

namespace Statmath.Application.DataHelper.Test
{
    public class JobConverterTest
    {
        public JobConverterTest()
        {
            _converter = new JobConverter();
        }

        public JobConverter _converter { get; }

        [Fact]
        public void ConvertFromCsvToViewModel()
        {
            var csvRow = @"MA-01;37293;2020-09-27-18-20;2020-09-27-19-25";
            var csvColumns = csvRow.Split(';');
            var vm = _converter.ConvertFromCsv(csvColumns);
            Assert.Equal("MA-01", vm.Machine);
            Assert.Equal(37293, vm.Job);
            Assert.Equal("2020-09-27-18-20", vm.Start);
            Assert.Equal("2020-09-27-19-25", vm.End);
        }

        [Fact]
        public void ConvertFromCsvToViewModelWithoutStart()
        {
            var csvRow = @"MA-01;37293;-;2020-09-27-19-25";
            var csvColumns = csvRow.Split(';');
            var vm = _converter.ConvertFromCsv(csvColumns);
            Assert.Equal("MA-01", vm.Machine);
            Assert.Equal(37293, vm.Job);
            Assert.Equal("-", vm.Start);
            Assert.Equal("2020-09-27-19-25", vm.End);
        }

        [Fact]
        public void ConvertFromCsvToViewModelWithoutEnd()
        {
            var csvRow = @"MA-01;37293;2020-09-27-18-20;-";
            var csvColumns = csvRow.Split(';');
            var vm = _converter.ConvertFromCsv(csvColumns);
            Assert.Equal("MA-01", vm.Machine);
            Assert.Equal(37293, vm.Job);
            Assert.Equal("2020-09-27-18-20", vm.Start);
            Assert.Equal("-", vm.End);
        }

        [Fact]
        public void ConvertFromCsvToViewModelWithoutDates()
        {
            var csvRow = @"MA-01;37293;-;-";
            var csvColumns = csvRow.Split(';');
            var vm = _converter.ConvertFromCsv(csvColumns);
            Assert.Equal("MA-01", vm.Machine);
            Assert.Equal(37293, vm.Job);
            Assert.Equal("-", vm.Start);
            Assert.Equal("-", vm.End);
        }
    }
}