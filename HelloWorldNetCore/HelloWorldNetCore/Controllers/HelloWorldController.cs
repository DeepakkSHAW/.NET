using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using HelloWorldNetCore.Model; //Need to add this explicitly.

namespace HelloWorldNetCore.Controllers
{
    // [Produces("application/json")]
    [Route("api/HelloWorld")]
    public class HelloWorldController : Controller
    {
        static List<Messages> _msg = new List<Messages>()
        {
            new Messages(){ ID = 1, Message = "Welcome there!"},
            new Messages(){ ID = 2, Message = "Helllo World.."},
        };

        [HttpGet]
        public IActionResult getWelcomeMsg()
        {
            return Ok(_msg[0].Message);
        }
        [HttpGet("getAllMessages")]
        public IActionResult getAllMeessages()
        {
            return Ok(_msg);
        }
        //[HttpGet("getaMessage")]
        [HttpGet]
        [Route("getaMessage/{id}")]
        public IActionResult getMessage([FromRoute] int id)
        {
            //return Ok("got it");
            Messages message = _msg.Find(item => item.ID == id);
            if (message != null)  return Ok(message);
            else return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpPost]
        public IActionResult PostMessage([FromBody] Messages msg)
        {
            _msg.Add(msg);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{ID}")]
        public IActionResult PutMessage([FromRoute] int id, [FromBody] Messages msg)
        {
            _msg[id] = msg;
            return StatusCode(StatusCodes.Status202Accepted);
        }
        [HttpDelete("{ID}")]
        public IActionResult DeleteMessage([FromRoute] int id)
        {
            _msg.RemoveAt(id);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}