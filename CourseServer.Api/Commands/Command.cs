using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CourseServer.Api.Commands
{
    public abstract class Command
    {
        public abstract Task<MasterMessage> Execute(string content);
    }
}
