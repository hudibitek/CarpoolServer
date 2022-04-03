using System;
using System.ComponentModel.DataAnnotations;

namespace CarPool.Models
{
    public class TravelPlanEmployeeCreateModel
    {
        [Required]
        public Guid? TravelPlanId { get; set; }

        [Required]
        public Guid? EmployeeId { get; set; }
    }
}
