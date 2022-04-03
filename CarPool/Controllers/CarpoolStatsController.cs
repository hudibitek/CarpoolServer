using CarPool.DataAccess.Entities;
using CarPool.FilterModels;
using CarPool.Models;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace CarPool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarpoolStatsController : ControllerBase
    {
        private readonly CarPoolContext _dbContext;
        private readonly ILogger<TravelPlansController> _logger;

        public CarpoolStatsController(CarPoolContext dbContext, ILogger<TravelPlansController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [Route("filter")]
        [HttpPost]
        public IActionResult Filter(TravelStatisticFilterModel filterModel)
        {         
            var carPassengers = _dbContext.Set<Car>().Join(
                                                  _dbContext.Set<TravelPlan>(),
                                                  car => car.Id,
                                                  tp => tp.CarId,
                                                  (car, tp) => new
                                                  {
                                                      Car = car,
                                                      TravelPlan = tp
                                                  }).Where(t => t.TravelPlan.StartDate.Month == filterModel.Month && t.TravelPlan.StartDate.Year == filterModel.Year).Join(
                                                    _dbContext.Set<TravelPlanEmployee>().DefaultIfEmpty(),
                                                    tp => tp.TravelPlan.Id,
                                                    tpe => tpe.TravelPlanId,
                                                    (carTravelPlan, passenger) => new
                                                    {
                                                        CarId = carTravelPlan.Car.Id,
                                                        CarName = carTravelPlan.Car.Name,
                                                        CarPlates = carTravelPlan.Car.Plates,
                                                        CarType = carTravelPlan.Car.CarType,
                                                        PassengerId = passenger.EmployeeId,
                                                        PassengerName = passenger.Employee.Name
                                                    }
                                                   );

            var travelPlans = _dbContext.Set<TravelPlan>().Where(tp => tp.StartDate.Month == filterModel.Month && tp.StartDate.Year == filterModel.Year)
                                                          .Select(tp => new { CarId = tp.CarId, DriverId = tp.DriverId, DriverName = tp.Driver.Name })
                                                          .ToList();
            
            
            var tripsByCars = _dbContext.Set<TravelPlan>().Where(tp => tp.StartDate.Month == filterModel.Month && tp.StartDate.Year == filterModel.Year)
                                                          .GroupBy(tp => tp.CarId)
                                                          .Select(g => new { CarId = g.Key, NumberOfTrips = g.Count() });

            var groupedCars = carPassengers.ToList().GroupBy(st => st.CarId);
            var statistics = new List<CarpoolStatModel>(); 
            foreach(var gr in groupedCars)
            {
                var items = gr.ToList();
                var firstItem = items.First();

                var header = new { Id = firstItem.CarId, CarName = firstItem.CarName, CarPlates = firstItem.CarPlates, NumberOfTrips = tripsByCars.First(tbc => tbc.CarId == firstItem.CarId).NumberOfTrips };
                var passengers = items.Select(i => i.PassengerName).ToList();
                passengers.AddRange(travelPlans.Where(tp => tp.CarId == header.Id).Select(tp => tp.DriverName));
                passengers = passengers.Distinct().ToList();
                statistics.Add(AutoMapper.Mapper.Map<CarpoolStatModel>(new { Car = header, UniquePassengers = passengers }));
            }
            
            return Ok(new { Data = statistics.ToList() });
        }       
    }
}
