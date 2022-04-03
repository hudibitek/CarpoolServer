using System;

namespace CarPool.DataAccess.Entities
{
    public class TravelPlanEmployee
    {
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public Guid TravelPlanId { get; set; }
        public virtual TravelPlan TravelPlan { get; set; }
    }
}
