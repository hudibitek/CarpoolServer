using AutoMapper;
using CarPool.DataAccess.Entities;
using CarPool.FilterModels;
using CarPool.Models;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace CarPool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TravelPlanEmployeesController : ControllerBase
    {
        private readonly CarPoolContext _dbContext;
        private readonly ILogger<TravelPlansController> _logger;

        public TravelPlanEmployeesController(CarPoolContext dbContext, ILogger<TravelPlansController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [Route("filter")]
        [HttpPost]
        public IActionResult Filter(TravelPlanEmployeeFilterModel filterModel)
        {
            var data = _dbContext.Set<TravelPlanEmployee>().Where(e => filterModel.TravelPlanId != null ? e.TravelPlanId == filterModel.TravelPlanId : true)
                                               .Include(e => e.Employee)
                                               .Include(e => e.TravelPlan)
                                               .OrderByDescending(e => e.Id);

            return Ok(new { Data = data.ToList() });
        }

        [HttpPost]
        public virtual IActionResult Create([FromBody] TravelPlanEmployeeCreateModel createModel)
        {
            var dbSet = _dbContext.Set<TravelPlanEmployee>();
            var currentPassengerCount = dbSet.Count(p => p.TravelPlanId == createModel.TravelPlanId);
            var maxSeats = _dbContext.Set<TravelPlan>().Where(tp => tp.Id == createModel.TravelPlanId).Include(tp => tp.Car).Select(tp => tp.Car.NumberOfSeats).FirstOrDefault();

            if (currentPassengerCount >= maxSeats - 1)
            {
                return BadRequest(new { errors = new string[] { "Max number of passengers reached. Remove some before adding more." } });
            }

            var alreadyAdded = dbSet.Any(tp => tp.TravelPlanId == createModel.TravelPlanId && tp.EmployeeId == createModel.EmployeeId);

            if (alreadyAdded)
            {
                return BadRequest(new { errors = new string[] { "Already added." } });
            }            

            var currentTravelPlan = _dbContext.Set<TravelPlan>().Where(tp => tp.Id == createModel.TravelPlanId).First();
            var alreadyOnATrip = dbSet.Any(tpe => tpe.TravelPlanId != currentTravelPlan.Id && tpe.EmployeeId == createModel.EmployeeId &&
                                                    !((tpe.TravelPlan.StartDate < currentTravelPlan.StartDate && tpe.TravelPlan.EndDate < currentTravelPlan.StartDate)
                                                     || (tpe.TravelPlan.StartDate > currentTravelPlan.EndDate && tpe.TravelPlan.EndDate > currentTravelPlan.EndDate)));

            if (alreadyOnATrip)
            {
                return BadRequest(new { errors = new string[] { "Already on a Travel plan that overlaps with this one." } });
            }

            if (currentTravelPlan.DriverId == createModel.EmployeeId)
            {
                return BadRequest(new { errors = new string[] { "Selected passenger is driving the car." } });
            }

            var travelPlanEmployee = Mapper.Map<TravelPlanEmployee>(createModel);
            dbSet.Add(travelPlanEmployee);
            _dbContext.SaveChanges();

            return Ok(new { Data = travelPlanEmployee.Id });
        }

        [HttpDelete("{id}")]
        public virtual IActionResult Delete(Guid id)
        {
            var dbSet = _dbContext.Set<TravelPlanEmployee>();
            var entity = dbSet.Find(id);
            dbSet.Remove(entity);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
