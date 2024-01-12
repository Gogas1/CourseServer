using CourseServer.Api.Commands.CommandsList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CourseServer.Api.Commands
{    
    public class MasterMessage
    {
        public string RequestId { get; set; }
        public string Command { get; set; }
        public string CommandData { get; set; }
    }
}
