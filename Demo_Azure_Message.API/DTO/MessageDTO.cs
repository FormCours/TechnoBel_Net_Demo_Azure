using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_Azure_Message.API.DTO
{
    public class MessageDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
