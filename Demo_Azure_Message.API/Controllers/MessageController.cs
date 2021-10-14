using Demo_Azure_Message.API.DTO;
using Demo_Azure_Message.DAL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            IEnumerable<MessageDTO> messages = _MessageService.GetAll().Select(m => new MessageDTO
            {
                Id = m.Id,
                Title = m.Title,
                Content = m.Content,
                Username = m.Username,
                SendDate = m.SendDate,
                UpdateDate = m.UpdateDate
            }) ;
            return Ok(messages);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(MessageCreateDTO message)
        {
            DAL.Entities.Message newMessage = new DAL.Entities.Message
            {
                Title = message.Title,
                Content = message.Content,
                Username = message.Username
            };

            Guid receivedId = _MessageService.Insert(newMessage);
            return Ok(receivedId);
        }
    }
}
