using System.Collections.Generic;

namespace CarPool.Models
{
    public class CarpoolStatModel
    {
        public CarStatModel Car { get; set; }

        public List<string> UniquePassengers { get; set; }
    }
}
