using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using RestSharp;
using FeedAPI.Entities;
using FeedAPI.DTOs;
using Microsoft.Extensions.Options;

namespace FeedAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly string databaseUri;

        public MessageController(IOptions<DatabaseOptions> options)
        {
            databaseUri = options.Value.BaseUri;
        }
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var client = new RestClient(databaseUri + "/message");
                var request = new RestRequest();

                var response = client.Get<List<Message>>(request);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound();
                }
                return Ok(response.Data);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetWithId(int id)
        {
            try
            {
                var client = new RestClient(databaseUri + "/message/" + id);
                var request = new RestRequest();

                var response =  client.Get<List<Message>>(request);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound();
                }
                return Ok(response.Data);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] PostMessageDTO postMessage)
        {
            try
            {
                var client = new RestClient(databaseUri);
                var request = new RestRequest("/message", Method.POST, DataFormat.Json);
                request.AddHeader("Accept", "application/json");
                request.AddJsonBody(new
                {
                    postMessage.Content,
                    postMessage.Author
                });
                var response = client.Execute(request);
                return Ok(response.Content);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpDelete]
        public ActionResult Delete([FromBody]DeleteMessageDTO deleteRequest)
        {
            try
            {
                var client = new RestClient(databaseUri);
                var request = new RestRequest("/message",Method.DELETE, DataFormat.Json);
                request.AddHeader("Accept", "application/json");
                request.AddJsonBody(new
                {
                    deleteRequest.Content
                });

                var response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound();
                }
                
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
