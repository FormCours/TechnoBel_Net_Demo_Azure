using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_Azure_Message.API.DTO
{
    public class MessageCreateDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }
    }
}
