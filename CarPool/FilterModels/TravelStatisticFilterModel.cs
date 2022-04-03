using System.ComponentModel.DataAnnotations;

namespace CarPool.FilterModels
{
    public class TravelStatisticFilterModel
    {
        [Required]
        public int? Year { get; set; }

        [Required]
        public int? Month { get; set; }
    }
}
