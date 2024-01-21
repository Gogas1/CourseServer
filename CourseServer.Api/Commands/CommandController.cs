using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;

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

        public async Task<MasterMessage> SendCommand(string command, string body)
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
                answer = await targetCommand.Execute(body);
            }

            return answer;
        }
    }
}
