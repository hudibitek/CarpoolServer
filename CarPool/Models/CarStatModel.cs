using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public class CarStatModel
    {
         public Guid Id { get; set; }
        public string CarName { get; set; }
        public string CarPlates { get; set; }
        public string NumberOfTrips { get; set; }
    }
}
