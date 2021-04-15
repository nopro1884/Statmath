using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Statmath.Application.Models;
using Statmath.Application.Repository.Abstraction;
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

        [ActionName("create_many")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] List<PlanViewModel> viewModels)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new System.Exception($"Objects of {nameof(PlanViewModel)} are not valid in {nameof(PlanController)}");
                }
                var models = _mapper.Map<List<PlanViewModel>, List<Plan>>(viewModels).ToList();
                var entriesWritten = await _planRepository.Add(models);
                return Ok($"{entriesWritten} plans stored");
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        public IActionResult Index()
        {
            return Ok();
        }
    }
}