using CourseServer.Api.Commands.CommandsList;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CourseServer.Api.Commands
{
    public class CommandController
    {
        private readonly ILogger<CommandController> _logger;
        private readonly IServiceProvider _serviceProvider;

        public CommandController(ILogger<CommandController> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public MasterMessage SendCommand(string command, string body)
        {
            Command? targetCommand = _serviceProvider.GetKeyedService<Command>(command.Trim('\0').ToLowerInvariant());

            var answer = new MasterMessage();

            if (targetCommand == null)
            {
                answer.Command = "wrongmessage";
                answer.CommandData = JsonSerializer.Serialize(command);
            }
            else
            {
                answer = targetCommand.Execute(body);                
            }            

            return answer;
        }
    }
}
