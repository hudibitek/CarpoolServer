using CarPool.DataAccess.Entities;
using CarPool.FilterModels;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace CarPool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly CarPoolContext _dbContext;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(CarPoolContext dbContext, ILogger<EmployeesController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [Route("filter")]
        [HttpPost]
        public IActionResult Filter(EmployeeFilterModel filter)
        {
            IQueryable<Employee> data = null;
            if (filter.OnlyDrivers != null)
            {
                data = _dbContext.Set<Employee>().Where(e => e.IsDriver == filter.OnlyDrivers);
            }
            else
            {
                data = _dbContext.Set<Employee>().Where(e => true).OrderBy(x => x.Name);
            }
            return Ok(new { Data = data.ToList() });
        }
    }
}
