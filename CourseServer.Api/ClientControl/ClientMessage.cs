using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Api.ClientControl
{
    public class ClientMessage
    {
        public Client Client { get; set; }
        public string Message { get; set; }

        public ClientMessage(string message, Client client)
        {
            Message = message;
            Client = client;
        }
    }
}
