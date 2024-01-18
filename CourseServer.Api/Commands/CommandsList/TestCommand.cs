using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CourseServer.Api.Commands.CommandsList
{
    public class TestCommand : Command
    {
        private readonly ILogger<TestCommand> _logger;
        public TestCommand(ILogger<TestCommand> logger)
        {
            _logger = logger;
        }

        public override async Task<MasterMessage> Execute(string content)
        {
            TestCommandData? data = JsonSerializer.Deserialize<TestCommandData>(content);

            MasterMessage result;
            if (data == null)
            {
                result = new MasterMessage{ Command = "wrongparameters", CommandData = "test" };
            }
            else
            {
                TestCommandAnswer testCommandAnswer = new TestCommandAnswer()
                {
                    DateTime = DateTime.Now,
                };

                result = new MasterMessage();
                result.Command = "testanswer";
                result.CommandData = JsonSerializer.Serialize(testCommandAnswer);

                _logger.LogInformation($"Command \"test\" has been executed with parameters {content}");
            }
            
            return result;
        }
    }

    public class TestCommandData
    {
        public int SomeInt { get; set; }
        public string SomeString { get; set; } = string.Empty;
    }

    public class TestCommandAnswer
    {
        public DateTime DateTime { get; set; }
    }
}
