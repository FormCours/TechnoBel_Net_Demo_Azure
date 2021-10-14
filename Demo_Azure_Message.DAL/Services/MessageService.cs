using Demo_Azure_Message.DAL.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Database.Interfaces;
using Toolbox.Database.Utils;

namespace Demo_Azure_Message.DAL.Services
{
    public interface IMessageService : ICrudService<Guid, Message>
    { }

    public class MessageService : IMessageService
    {
        private Connection _Connection;
        private string _ConnectionString;

        public MessageService(IConfiguration config)
        {
            _ConnectionString = config.GetConnectionString("default");
            _Connection = new Connection(SqlClientFactory.Instance, _ConnectionString);
        }

        private Message MapToMessage(IDataRecord record)
        {
            return new Message
            {
                Id = (Guid)record["Id"],
                Title = record["Title"].ToString(),
                Content = record["Content"].ToString(),
                Username = record["Username"].ToString(),
                SendDate = (DateTime)record["SendDate"],
                UpdateDate = record["UpdateDate"] is DBNull ? null : (DateTime)record["UpdateDate"]
            };
        }

        public IEnumerable<Message> GetAll()
        {
            Command command = new Command("SELECT * FROM [Message]");

            _Connection.Open();
            foreach (var item in _Connection.ExecuteReader(command, MapToMessage))
            {
                yield return item;
            }
            _Connection.Close();
        }

        public Message GetById(Guid id)
        {
            Command command = new Command("SELECT * FROM [Message] WHERE [Id] = @Id");
            command.AddParameter("Id", id);

            _Connection.Open();
            Message msg = _Connection.ExecuteReader(command, MapToMessage).SingleOrDefault();
            _Connection.Close();

            return msg;
        }

        public Guid Insert(Message entity)
        {
            Command command = new Command("INSERT INTO [Message] ([Title], [Content], [Username]) " +
                                            "OUTPUT [inserted].[Id] " +
                                            "VALUES (@Title, @Content, @Username);");
            command.AddParameter("Title", entity.Title);
            command.AddParameter("Content", entity.Content);
            command.AddParameter("Username", entity.Username);

            _Connection.Open();
            Guid newId = _Connection.ExecuteScalar<Guid>(command);
            _Connection.Close();

            return newId;
        }


        public bool Update(Guid id, Message entity)
        {
            throw new NotImplementedException();
        }
        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
