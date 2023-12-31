using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public string SendCommand(string command, string body)
        {
            Command targetCommand = _serviceProvider.GetRequiredKeyedService<Command>(command.ToLower());

            string result = targetCommand.Execute(body);
            return result;
        }
    }
}
