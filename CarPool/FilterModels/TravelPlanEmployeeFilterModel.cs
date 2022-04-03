using System;
using System.ComponentModel.DataAnnotations;

namespace CarPool.FilterModels
{
    public class TravelPlanEmployeeFilterModel
    {
        [Required]
        public Guid? TravelPlanId { get; set; }
    }
}
