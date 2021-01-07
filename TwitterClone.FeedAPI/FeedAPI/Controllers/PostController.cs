using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using RestSharp;

namespace FeedAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private const string DbUri = "DB_URI";
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var dbURI = Environment.GetEnvironmentVariable(DbUri);
            dbURI += "/weatherforecast";

            try
            {
                var client = new RestClient(dbURI);
                var request = new RestRequest();

                var response = await client.GetAsync<List<WeatherForecast>>(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Debug.WriteLine(e.Message);
                throw;
            }
        }
    }
}
