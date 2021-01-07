using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace FeedAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private const string DbUri = "DB_URI";

        [HttpGet]
        public ActionResult Get()
        {
            var dbURI = Environment.GetEnvironmentVariable(DbUri);
            dbURI += "/databasetest";
            var client = new RestClient(dbURI);
            var request = new RestRequest();

            var response =  client.Get(request);
            return Ok(response.Content);
        }
    }
}
