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
            twitterCloneDbContext.Database.EnsureCreated();
            return Ok(twitterCloneDbContext.Messages);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetWithId(int id)
        {
            return Ok(twitterCloneDbContext.Messages.SingleOrDefault(Message => Message.Id == id));
        }

        [HttpPost]
        public ActionResult Post([FromBody] MessageDTO message)
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
    }
}
