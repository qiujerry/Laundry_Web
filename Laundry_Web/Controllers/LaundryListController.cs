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
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<LaundryListController> _logger;

        public LaundryListController(ILogger<LaundryListController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<LaundryList> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new LaundryList
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
