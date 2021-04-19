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
    public class PlanController : Controller
    {
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;

        public PlanController(IPlanRepository planRepository, IMapper mapper)
        {
            _planRepository = planRepository;
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
        public async Task<IActionResult> DeleteAsync([FromBody] PlanViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception($"Objects of {nameof(Int32)} are not valid in {nameof(PlanController)}");
                }
                var model = _mapper.Map<PlanViewModel, PlanDto>(viewModel);
                var affectedRow = await _planRepository.Delete(model);
                return Ok(affectedRow);
            }
            catch (System.Exception)
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
                var affectedRow = await _planRepository.Delete();
                return Ok(affectedRow);
            }
            catch (System.Exception)
            {
                return BadRequest("Unable to store data to database");
            }
        }

        [ActionName(Constants.ApiActionCreate)]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] PlanViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new System.Exception($"Objects of {nameof(PlanViewModel)} are not valid in {nameof(PlanController)}");
                }
                var model = _mapper.Map<PlanViewModel, PlanDto>(viewModel);
                var entriesWritten = await _planRepository.Add(model);
                return Ok($"{entriesWritten} plans stored");
            }
            catch (System.Exception)
            {
                return BadRequest("Unable to store data to database");
            }
        }

        [ActionName(Constants.ApiActionCreateMany)]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] List<PlanViewModel> viewModels)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new System.Exception($"Objects of {nameof(PlanViewModel)} are not valid in {nameof(PlanController)}");
                }
                var models = _mapper.Map<List<PlanViewModel>, List<PlanDto>>(viewModels).ToList();
                var entriesWritten = await _planRepository.Add(models);
                return Ok($"{entriesWritten} plans stored");
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
                var plan = _planRepository.GetByJob(j);
                var planVm = _mapper.Map<PlanViewModel>(plan);
                return Ok(planVm);
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
                var models = _planRepository.GetByMachineName(m);
                var viewModels = _mapper.Map<List<PlanDto>, List<PlanViewModel>>(models?.ToList());
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
                var models = default(List<PlanDto>);
                switch (t)
                {
                    case "start":
                        models = _planRepository.GetByStartDate(d).ToList();
                        break;
                    case "end":
                        models = _planRepository.GetByEndDate(d).ToList();
                        break;
                    default:
                        return BadRequest(CreateUnableGetDataMessage(t));
                }
                
                var viewModels = _mapper.Map<List<PlanDto>, List<PlanViewModel>>(models);
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
                var models = default(List<PlanDto>);
                switch (t)
                {
                    case "start":
                        models = _planRepository.GetByStartDateTime(d).ToList();
                        break;
                    case "end":
                        models = _planRepository.GetByEndDateTime(d).ToList();
                        break;
                    default:
                        return BadRequest(CreateUnableGetDataMessage(t));
                }

                var viewModels = _mapper.Map<List<PlanDto>, List<PlanViewModel>>(models);
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
                var models = _planRepository.GetAll();
                var viewModels = _mapper.Map<List<PlanDto>, List<PlanViewModel>>(models?.ToList());
                return Ok(viewModels);
            }
            catch (Exception)
            {
                return BadRequest(Constants.UnableToGetDataMessage);
            }
        }
    }
}