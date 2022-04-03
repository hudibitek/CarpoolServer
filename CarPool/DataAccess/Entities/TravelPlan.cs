using System;

namespace CarPool.DataAccess.Entities
{
    public class TravelPlan
    {
        public Guid Id { get; set; }
        public string StartLocation { get; set; }

        public string EndLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Guid CarId { get; set; }
        public virtual Car Car { get; set; }        

        public Guid DriverId { get; set; }
        public Employee Driver { get; set; }
    }
}
