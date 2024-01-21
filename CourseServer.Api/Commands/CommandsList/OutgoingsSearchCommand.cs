using CourseServer.Core.Interfaces.Services;
using System.Text.Json;

namespace CourseServer.Api.Commands.CommandsList
{
    public class OutgoingsSearchCommand : Command
    {
        private readonly IOutgoingService _outgoingService;

        public OutgoingsSearchCommand(IOutgoingService outgoingService)
        {
            _outgoingService = outgoingService;
        }

        public override async Task<MasterMessage> Execute(string content)
        {
            CommandData commandData = JsonSerializer.Deserialize<CommandData>(content ?? string.Empty) ?? new CommandData();

            var outgoings = await _outgoingService.GetOutgoingsBeetweenDates(commandData.StartDate, commandData.EndDate);
            CommandAnswer commandAnswer = new CommandAnswer();
            foreach (var outgoing in outgoings)
            {
                OutgoingRecord newIncomeRecord = new OutgoingRecord
                {
                    Id = outgoing.Id,
                    CreatedAt = outgoing.DateTime,
                    Summ = outgoing.OutgoingProducts.Sum(o => o.Price * (decimal)o.Amount),
                    Manager = outgoing.Manager
                };

                commandAnswer.OutgoingFound.Add(newIncomeRecord);
            }

            return new MasterMessage { Command = "outgoings_found", CommandData = JsonSerializer.Serialize(commandAnswer) };
        }

        private class CommandData
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        private class CommandAnswer
        {
            public List<OutgoingRecord> OutgoingFound { get; set; } = new();
        }
        private class OutgoingRecord
        {
            public int Id { get; set; }
            public DateTime CreatedAt { get; set; }
            public string Manager { get; set; } = string.Empty;
            public decimal Summ { get; set; }
        }
    }
}
