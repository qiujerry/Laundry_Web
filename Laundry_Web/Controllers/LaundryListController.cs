using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

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
            LaundryDatabaseConnection x = new LaundryDatabaseConnection();
            ArrayList data = x.laundryDatabaseDownload();
            return Enumerable.Range(1, 15).Select(index => new LaundryList
            {
                MachineNumber = index,
                Date = (data[index - 1] as LaundryList).Date,
                TimeSet = (data[index - 1] as LaundryList).TimeSet,
                Available = (data[index - 1] as LaundryList).Available
            }).ToArray();
        }

        [HttpPost]
        public async Task<ActionResult<LaundryList>> Post(LaundryList list)
        {

            LaundryDatabaseConnection x = new LaundryDatabaseConnection();
            x.laundryDatabaseUpload(list.MachineNumber, list.TimeSet, list.Available);

            return CreatedAtAction(nameof(list), new {}, list);

        }


    }
}
