using Microsoft.AspNetCore.Mvc;
using Statmath.Application.Task.Data.Context;
using Statmath.Application.Task.Models;
using System;
using System.Linq;

namespace Statmath.Application.Task.Api.Controllers
{
    public class PlanController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public PlanController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            _dbContext.Add(new Plan
            {
                Machine = "Test",
                Job = 11111,
                StartedAt = DateTime.Now,
                EndedAt = DateTime.MaxValue
            });

            _dbContext.SaveChanges();


            return Ok();
        }
    }
}
