using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Statmath.Application.Task.Data.Context;

namespace Statmath.Application.Task.Api.Controllers
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

        public IActionResult Index()
        {
            return Ok();
        }
    }
}
