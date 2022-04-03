using CarPool.DataAccess.Entities;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace CarPool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly CarPoolContext _dbContext;
        private readonly ILogger<CarsController> _logger;

        public CarsController(CarPoolContext dbContext, ILogger<CarsController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [Route("filter")]
        [HttpPost]
        public IActionResult Filter()
        {
            var data = _dbContext.Set<Car>().Where(e => true);

            return Ok(new { Data = data.ToList() });
        }
    }
}
