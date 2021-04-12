using Statmath.Application.Task.DataHelper.Abstraction;
using Statmath.Application.Task.Models;

namespace Statmath.Application.Task.DataHelper.Implementation
{
    public class PlanConverter : IPlanConverter
    {
        public PlanViewModel ConvertFromCsv(string[] fields)
        {
            // make sure that the structure is complete
            // and the job could be converted into integer
            if (fields.Length > 3 && int.TryParse(fields[1], out var job))
            {
                // 
                // job == 37349 || job == 37366 -> test conditions
                
                return new PlanViewModel
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
