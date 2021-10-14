using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Database.Interfaces;

namespace Demo_Azure_Message.DAL.Entities
{
    public class Message : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}