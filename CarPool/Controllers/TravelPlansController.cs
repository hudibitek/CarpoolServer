using AutoMapper;
using CarPool.DataAccess.Entities;
using CarPool.Models;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarPool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TravelPlansController : ControllerBase
    {
        private readonly CarPoolContext _dbContext;
        private readonly ILogger<TravelPlansController> _logger;

        public TravelPlansController(CarPoolContext dbContext, ILogger<TravelPlansController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {            
            var data = _dbContext.Set<TravelPlan>().Where(e => e.Id == id)
                                               .Include(e => e.Driver)
                                               .Include(e => e.Car)
                                               .FirstOrDefault();

            return Ok(new { Data = data });
        }

        [Route("filter")]
        [HttpPost]
        public IActionResult Filter()
        {
            // nema paginga skip/take
            var data = _dbContext.Set<TravelPlan>().Where(e => true)
                                               .Include(e => e.Driver)
                                               .Include(e => e.Car)
                                               .OrderByDescending(e => e.StartDate);

            return Ok(new { Data = data.ToList() });
        }

        [HttpPost]
        public virtual IActionResult Create([FromBody] TravelPlanCreateModel createModel)
        {
            var error = createModel.Validate(_dbContext);
            if (error != null)
            {
                return BadRequest(new { errors = new string[] { error } });
            }

            var travelPlan = Mapper.Map<TravelPlan>(createModel);
            _dbContext.Set<TravelPlan>().Add(travelPlan);            
            _dbContext.SaveChanges();

            return Ok(new { Data = travelPlan.Id });
        }

        [HttpPut]
        public virtual IActionResult Update([FromBody] TravelPlanUpdateModel updateModel)
        {
            var error = updateModel.Validate(_dbContext);
            if (error != null)
            {
                return BadRequest(error);
            }

            var travelPlan = Mapper.Map<TravelPlan>(updateModel);
            _dbContext.Set<TravelPlan>().Update(travelPlan);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public virtual IActionResult Delete(Guid id)
        {
            var dbSet = _dbContext.Set<TravelPlan>();
            var entity = dbSet.Find(id);
            dbSet.Remove(entity);
            _dbContext.SaveChanges();

            return Ok();
        }       
    }
}
