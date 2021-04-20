using Statmath.Application.DataHelper.Abstraction;
using Statmath.Application.Models;

namespace Statmath.Application.DataHelper.Implementation
{
    public class JobConverter : IJobConverter
    {
        public JobViewModel ConvertFromCsv(string[] fields)
        {
            // make sure that the structure is complete
            // and the job could be converted into integer
            if (fields.Length == 4 && int.TryParse(fields[1], out var job))
            {
                return new JobViewModel
                {
                    Machine = fields[0],
                    Job = job,
                    Start = fields[2],
                    End = fields[3]
                };
            }
            throw new System.Exception("Unable to convert row from csv file");
        }
    }
}