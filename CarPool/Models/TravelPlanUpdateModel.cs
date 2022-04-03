using CarPool.DataAccess.Entities;
using DataAccess;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CarPool.Models
{
    public class TravelPlanUpdateModel
    {
        public Guid Id { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Required]
        public Guid? CarId { get; set; }

        [Required]
        public Guid? DriverId { get; set; }

        public string Validate(CarPoolContext dbContext)
        {
            if (StartDate >= EndDate)
            {
                return "StartDate has to be less than EndDate";
            }

            if (StartLocation == EndLocation)
            {
                return "Start and end location can't be same";
            }
            
            var canDriverDrive = dbContext.Set<Employee>().Any(e => e.IsDriver && DriverId == e.Id);
            if (!canDriverDrive)
            {
                return "Employee is not a driver.";
            }

            var isCarBusy = dbContext.Set<TravelPlan>().Any(t => t.CarId == CarId && t.Id != Id &&
                                                            ((t.StartDate >= StartDate && t.StartDate <= EndDate)
                                                            || (t.EndDate >= StartDate && t.EndDate <= EndDate)));

            if (isCarBusy)
            {
                return "Selected car is already on a trip in the selected time range.";
            }

            return null;
        }
    }
}
