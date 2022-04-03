using System;

namespace CarPool.DataAccess.Entities
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CarType { get; set; }
        public string Color { get; set; }
        public string Plates { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
