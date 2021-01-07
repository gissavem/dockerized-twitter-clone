using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TwitterClone.Database.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatabaseTestController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("From the database test controller");
        }
    }
}
