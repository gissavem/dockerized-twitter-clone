using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.Database.DataAccessLayer;
using TwitterClone.Database.DTOs;

namespace TwitterClone.Database.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly TwitterCloneDbContext twitterCloneDbContext;

        public MessageController(TwitterCloneDbContext twitterCloneDbContext)
        {
            this.twitterCloneDbContext = twitterCloneDbContext;
        }
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(twitterCloneDbContext.Messages);
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
                var message = twitterCloneDbContext.Messages.SingleOrDefault(message => message.Id == id);
                if (message is null)
                {
                    return NotFound();
                }
                return Ok(message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] MessageDTO message)
        {
            try
            {
                var messageToAdd = new Message
                {
                    Author = message.Author,
                    Content = message.Content,
                    PostDate = DateTime.Now
                };
                twitterCloneDbContext.Add(messageToAdd);
                twitterCloneDbContext.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpDelete]
        public ActionResult Delete([FromBody]DeleteRequest deleteRequest)
        {
            try
            {
                var messageToRemove = twitterCloneDbContext.Messages.SingleOrDefault(message => message.Content == deleteRequest.Content);
                if (messageToRemove is null)
                {
                    return NotFound();
                }
                twitterCloneDbContext.Messages.Remove(messageToRemove);
                twitterCloneDbContext.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }

    public class DeleteRequest
    {
        public string Content { get; set; }
    }
}
