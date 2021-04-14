using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Statmath.Application.Data.Context;
using Statmath.Application.Models;
using System.Linq;

namespace Statmath.Application.Api.Controllers
{
    public class PlanController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public PlanController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        //public async Task<IActionResult> Insert([FromBody] PlanViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Invalid data received");
        //    }


            
        //}
        

        public IActionResult Index()
        {
            var plan = new Plan
            {
                Machine = "TEst",
                Job = 12345,
                StartedAt = new System.DateTime(1984, 08, 18, 18, 30, 0),
                EndedAt = new System.DateTime(1984, 08, 20, 18, 30, 0)
            };

            _dbContext.Plans.Add(plan);
            
            _dbContext.SaveChanges();

            var plans = _dbContext.Plans.ToList();

            return Ok();
        }

        [HttpGet(Name = "getall")]
        public IActionResult GetAll()
        {
            var plans = _dbContext.Plans.ToList();
            return Ok(plans);
        }
    }
}