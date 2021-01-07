using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using RestSharp;
using FeedAPI.Entities;
using FeedAPI.DTOs;

namespace FeedAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private const string DbUri = "DB_URI";
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var dbURI = Environment.GetEnvironmentVariable(DbUri);
            dbURI += "/message";

            try
            {
                var client = new RestClient(dbURI);
                var request = new RestRequest();

                var response = await client.GetAsync<List<Message>>(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetWithId(int id)
        {
            var dbURI = Environment.GetEnvironmentVariable(DbUri);
            dbURI += "/message/" + id;

            try
            {
                var client = new RestClient(dbURI);
                var request = new RestRequest();

                var response = await client.GetAsync<List<Message>>(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] MessageDTO message)
        {
            var dbURI = Environment.GetEnvironmentVariable(DbUri);
            try
            {
                var client = new RestClient(dbURI);
                var request = new RestRequest("/message", Method.POST, DataFormat.Json);
                request.AddHeader("Accept", "text/plain");
                request.AddJsonBody(new
                {
                    Content = message.Content,
                    Author = message.Author
                });
                var response = client.Execute(request);
                Console.WriteLine(response.ErrorMessage);
                Debug.WriteLine(response.ErrorMessage);
                return Ok(response.Content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Debug.WriteLine(e.Message);
                return StatusCode(500);
            }
        }
    }
}
