using Statmath.Application.DataHelper.Abstraction;
using Statmath.Application.Exceptions;
using Statmath.Application.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Statmath.Application.DataHelper.Implementation
{
    public class CsvHelper : ICsvHelper
    {
        private readonly IJobConverter _converter;

        public CsvHelper(IJobConverter converter)
        {
            _converter = converter;
        }

        public bool IsFileNotInUse(string filePath)
        {
            try
            {
                // try to open a read only file stream 
                // stream will be close instantly after creation
                FileInfo file = new FileInfo(filePath);
                file.Open(FileMode.Open, FileAccess.Read, FileShare.None).Close();
                return true;
            }
            catch (IOException)
            {
                throw new FileAlreadyInUseException(filePath);
            }
        }

        public IEnumerable<JobViewModel> ReadCsvFile(string filePath, bool isHeaderIncluded = false)
        {
            try
            {
                var lines = File.ReadAllLines(filePath, Encoding.UTF8);
                var vms = from line in isHeaderIncluded ? lines : lines.Skip(1)
                          let fields = line.Split(';')
                          select _converter.ConvertFromCsv(fields);
                return vms;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
