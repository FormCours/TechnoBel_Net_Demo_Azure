using Demo_Azure_Message.DAL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_Azure_Message.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IMessageService _MessageService;

        public MessageController(IMessageService messageService)
        {
            _MessageService = messageService;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            // TODO Add models in API
            var messages = _MessageService.GetAll();
            return Ok(messages);
        }
    }
}
