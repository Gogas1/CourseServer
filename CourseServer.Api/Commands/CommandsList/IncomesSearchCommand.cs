using CourseServer.Core.Interfaces.Services;
using System.Text.Json;

namespace CourseServer.Api.Commands.CommandsList
{
    public class IncomesSearchCommand : Command
    {
        private readonly IIncomeService _incomeService;

        public IncomesSearchCommand(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        public override async Task<MasterMessage> Execute(string content)
        {
            CommandData? data = JsonSerializer.Deserialize<CommandData>(content);

            if (data == null)
            {
                return new MasterMessage { Command = "incomesearch_wrongparams", CommandData = "wrongformat" };
            }
            else
            {
                var incomes = await _incomeService.GetIncomesBeetweenDates(data.StartDate, data.EndDate);

                CommandAnswer commandAnswer = new CommandAnswer();
                foreach (var income in incomes)
                {
                    IncomeRecord newIncomeRecord = new IncomeRecord
                    {
                        Id = income.Id,
                        CreatedAt = income.CreatedAt,
                        Summ = income.Summ,
                        Supplier = income.Supplier
                    };

                    commandAnswer.IncomesFound.Add(newIncomeRecord);
                }

                return new MasterMessage { Command = "incomes_found", CommandData = JsonSerializer.Serialize(commandAnswer) };
            }
        }

        private class CommandData
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        private class CommandAnswer
        {
            public List<IncomeRecord> IncomesFound { get; set; } = new();
        }
        private class IncomeRecord
        {
            public int Id { get; set; }
            public DateTime CreatedAt { get; set; }
            public string Supplier { get; set; } = string.Empty;
            public decimal Summ { get; set; }
        }
    }
}
