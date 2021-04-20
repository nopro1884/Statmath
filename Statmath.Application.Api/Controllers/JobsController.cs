using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Statmath.Application.Models;
using Statmath.Application.Repository.Abstraction;
using Statmath.Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Statmath.Application.Api.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;

        public JobsController(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        private string CreateUnableGetDataMessage(dynamic parameter)
        {
            var msg = Constants.UnableToGetDataMessagePlaceholder
                    .Replace(Constants.UnableToGetDataMessagePlaceholder,
                        parameter is string ? parameter : Convert.ToString(parameter));
            return msg;
        }

        [ActionName(Constants.ApiActionDelete)]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] JobViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception($"Objects of {nameof(Int32)} are not valid in {nameof(JobsController)}");
                }
                var model = _mapper.Map<JobViewModel, JobDto>(viewModel);
                var affectedRow = await _jobRepository.Delete(model);
                return Ok(affectedRow);
            }
            catch (Exception)
            {
                return BadRequest("Unable to store data to database");
            }
        }

        [ActionName(Constants.ApiActionDeleteMany)]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            try
            {
                var affectedRow = await _jobRepository.Delete();
                return Ok(affectedRow);
            }
            catch (System.Exception)
            {
                return BadRequest("Unable to store data to database");
            }
        }

        [ActionName(Constants.ApiActionCreate)]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] JobViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new System.Exception($"Objects of {nameof(JobViewModel)} are not valid in {nameof(JobsController)}");
                }
                var model = _mapper.Map<JobViewModel, JobDto>(viewModel);
                var entriesWritten = await _jobRepository.Add(model);
                return Ok($"{entriesWritten} jobs stored");
            }
            catch (System.Exception)
            {
                return BadRequest("Unable to store data to database");
            }
        }

        [ActionName(Constants.ApiActionCreateMany)]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] List<JobViewModel> viewModels)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new System.Exception($"Objects of {nameof(JobViewModel)} are not valid in {nameof(JobsController)}");
                } 
                var models = _mapper.Map<List<JobViewModel>, List<JobDto>>(viewModels).ToList();
                var entriesWritten = await _jobRepository.Add(models);
                return Ok($"{entriesWritten} jobs stored");
            }
            catch (System.Exception)
            {
                return BadRequest("Unable to store data to database");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="j">job id</param>
        /// <returns></returns>
        [ActionName(Constants.ApiActionGetByJob)]
        [HttpGet]
        public IActionResult GetByJob([FromQuery] int j)
        {
            try
            {
                var job = _jobRepository.GetByJob(j);
                var jobVm = _mapper.Map<JobViewModel>(job);
                return Ok(jobVm);
            }
            catch (Exception)
            {
                return BadRequest(CreateUnableGetDataMessage(j));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m">machine id</param>
        /// <returns></returns>
        [ActionName(Constants.ApiActionGetByMachine)]
        [HttpGet]
        public IActionResult GetByMachine([FromQuery] string m)
        {
            try
            {
                var models = _jobRepository.GetByMachineName(m);
                var viewModels = _mapper.Map<List<JobDto>, List<JobViewModel>>(models?.ToList());
                return Ok(viewModels);
            }
            catch (Exception)
            {
                return BadRequest(CreateUnableGetDataMessage(m));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t">type of job state -> like end or start</param>
        /// <param name="d"></param>
        /// <returns></returns>
        [ActionName(Constants.ApiActionGetByDate)]
        [HttpGet]
        public IActionResult GetByDate([FromQuery] string t, [FromQuery] string d)
        {
            try
            {
                var models = default(List<JobDto>);
                switch (t)
                {
                    case "start":
                        models = _jobRepository.GetByStartDate(d).ToList();
                        break;
                    case "end":
                        models = _jobRepository.GetByEndDate(d).ToList();
                        break;
                    default:
                        return BadRequest(CreateUnableGetDataMessage(t));
                }
                
                var viewModels = _mapper.Map<List<JobDto>, List<JobViewModel>>(models);
                return Ok(viewModels);
            }
            catch (Exception)
            {
                return BadRequest(CreateUnableGetDataMessage(d));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t">type of job state -> like end or start</param>
        /// <param name="d"></param>
        /// <returns></returns>
        [ActionName(Constants.ApiActionGetByDateTime)]
        [HttpGet]
        public IActionResult GetByDateTime([FromQuery] string t, [FromQuery] string d)
        {
            try
            {
                var models = default(List<JobDto>);
                switch (t)
                {
                    case "start":
                        models = _jobRepository.GetByStartDateTime(d).ToList();
                        break;
                    case "end":
                        models = _jobRepository.GetByEndDateTime(d).ToList();
                        break;
                    default:
                        return BadRequest(CreateUnableGetDataMessage(t));
                }

                var viewModels = _mapper.Map<List<JobDto>, List<JobViewModel>>(models);
                return Ok(viewModels);
            }
            catch (Exception)
            {
                return BadRequest(CreateUnableGetDataMessage(d));
            }
        }



        [ActionName(Constants.ApiActionGetAll)]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var models = _jobRepository.GetAll();
                var viewModels = _mapper.Map<List<JobDto>, List<JobViewModel>>(models?.ToList());
                return Ok(viewModels);
            }
            catch (Exception)
            {
                return BadRequest(Constants.UnableToGetDataMessage);
            }
        }
    }
}