using Statmath.Application.DataHelper.Abstraction;
using Statmath.Application.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Statmath.Application.DataHelper.Implementation
{
    public class CsvHelper : ICsvHelper
    {
        private readonly IPlanConverter _converter;

        public CsvHelper(IPlanConverter converter)
        {
            _converter = converter;
        }

        public bool IsFileInUse(string filePath)
        {
            try
            {
                FileInfo file = new FileInfo(filePath);
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
                return false;
            }
            catch (IOException)
            {
                return true;
            }
        }

        public IEnumerable<PlanViewModel> ReadCsvFile(string filePath, bool isHeaderIncluded = false)
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
