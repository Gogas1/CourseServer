using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Api.Commands
{
    public class TestCommand : Command
    {
        private readonly ILogger _logger;
        public TestCommand(ILogger logger)
        {
            _logger = logger;
        }

        public override string Execute(string content) 
        {
            string result = "Test command executed";
            _logger.LogInformation(result);

            return result;
        }
    }
}
