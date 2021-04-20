using Statmath.Application.Data.Context;
using Statmath.Application.DataHelper.Abstraction;
using Statmath.Application.Models;
using Statmath.Application.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Statmath.Application.Repository.Implementation
{
    public class JobRepository : IJobRepository
    {
        public ApplicationDbContext _context { get; }
        public IDateTimeHelper _dateTimeHelper { get; }
        public IDateTimeConverter _dateTimeConverter { get; }

        public JobRepository(
            ApplicationDbContext context,
            IDateTimeHelper dateTimeHelper,
            IDateTimeConverter dateTimeConverter
            )
        {
            _context = context;
            _dateTimeHelper = dateTimeHelper;
            _dateTimeConverter = dateTimeConverter;
        }

        // add a single job to data base
        public async Task<int> Add(JobDto dto)
        {
            try
            {
                // check if machine is existing
                var machine = _context.Machines.FirstOrDefault(x => x.Name == dto.Machine.Name);
                if (machine == default(MachineDto))
                {
                    // machine not exisiting -> create one
                    _context.Machines.Add(new MachineDto
                    {
                        Name = dto.Machine.Name,
                        Jobs = new[] { dto }
                    });
                }
                else
                {
                    // machine exisiting -> update 
                    machine.Jobs.Add(dto);
                }
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Add(IEnumerable<JobDto> dtos)
        {
            try
            {
                // get machine distinct
                var machinesNames = dtos.Select(x => x.Machine.Name).Distinct();
                var machines = new List<MachineDto>();
                foreach (var machineName in machinesNames)
                {
                    var jobs = dtos.Where(x => x.Machine.Name == machineName).ToList();
                    var machine = new MachineDto
                    {
                        Name = machineName,
                        Jobs = jobs
                    };
                    machines.Add(machine);
                }
                _context.Machines.AddRange(machines);
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // delete a job
        public async Task<int> Delete(JobDto dto)
        {
            try
            {
                var job = _context.Jobs.Where(x => x.Id == dto.Id);
                if (job != default(JobDto))
                {
                    _context.Jobs.Remove(dto);
                    return await _context.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // delete all jobs 
        public async Task<int> Delete()
        {
            try
            {
                var jobs = _context.Jobs;
                if (jobs?.Any() ?? false)
                {
                    _context.Jobs.RemoveRange(jobs);
                    return await _context.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // get all jobs
        public IEnumerable<JobDto> GetAll()
        {
            var list = _context.Jobs.ToList();
            LoadMachineLazy(list);

            return list;
        }

        // get jobs by end date
        public IEnumerable<JobDto> GetByEndDate(string date)
        {
            var dateTime = _dateTimeConverter.ConvertToDateTime(date);
            var list = _context.Jobs.ToList().Where(p => _dateTimeHelper.IsDayEqual(p.EndedAt, dateTime));
            LoadMachineLazy(list);

            return list;
        }

        // get jobs by start date
        public IEnumerable<JobDto> GetByStartDate(string date)
        {
            var dateTime = _dateTimeConverter.ConvertToDateTime(date);
            var list = _context.Jobs.ToList().Where(p => _dateTimeHelper.IsDayEqual(p.StartedAt, dateTime));
            LoadMachineLazy(list);

            return list;
        }

        // get jobs by end time
        public IEnumerable<JobDto> GetByEndDateTime(string date)
        {
            var dateTime = _dateTimeConverter.ConvertToDateTime(date);
            var list = _context.Jobs.ToList().Where(p => p.EndedAt.Equals(dateTime));
            LoadMachineLazy(list);

            return list;
        }

        // get jobs by start time
        public IEnumerable<JobDto> GetByStartDateTime(string date)
        {
            var dateTime = _dateTimeConverter.ConvertToDateTime(date);
            var list = _context.Jobs.Where(p => p.StartedAt.Equals(dateTime)).ToList();
            LoadMachineLazy(list);

            return list;
        }

        // get job by job number
        public JobDto GetByJob(int job)
        {
            var dto = _context.Jobs.FirstOrDefault(p => p.Job == job);
            if (dto == default(JobDto))
                dto.Machine = _context.Machines.FirstOrDefault(m => m.Id == dto.MachineId);
            return dto;
        }

        // get jobs by machine name
        public IEnumerable<JobDto> GetByMachineName(string machine)
        {
            var jobs = GetAll();
            return jobs.Where(p => p.Machine.Name == machine);
        }


        // load necessary machines depending on jobs
        private void LoadMachineLazy(IEnumerable<JobDto> jobs)
        {
            // exit if list null or empty
            if (jobs?.Any() ?? false)
                return;

            foreach (var job in jobs)
            {
                job.Machine = _context.Machines.FirstOrDefault(x => x.Id == job.MachineId);
            }
        }
    }
}