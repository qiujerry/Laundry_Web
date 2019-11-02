using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Laundry_Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LaundryListController : ControllerBase
    {
        private readonly ILogger<LaundryListController> _logger;

        public LaundryListController(ILogger<LaundryListController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<LaundryList> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 15).Select(index => new LaundryList
            {
                MachineNumber = index,
                Date = DateTime.Now.AddMinutes(rng.Next(-40, 0)),
                TimeSet = rng.Next(10, 60),
                Available = "closed"
            }).ToArray();
        }
    }
}
