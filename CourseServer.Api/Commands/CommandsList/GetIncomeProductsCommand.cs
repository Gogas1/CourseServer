using CourseServer.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourseServer.Api.Commands.CommandsList
{
    public class GetIncomeProductsCommand : Command
    {
        private readonly IIncomeService _incomeService;

        public GetIncomeProductsCommand(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        public override async Task<MasterMessage> Execute(string content)
        {
            var id = JsonSerializer.Deserialize<int>(content);

            if(id == 0)
            {
                return ReturnEmptyMessage();
            }
            else
            {
                var targetIncome = await _incomeService.GetIncomeById(id);
                if(targetIncome == null)
                {
                    return ReturnEmptyMessage();
                }
                else
                {
                    CommandAnswer commandAnswer = new CommandAnswer();
                    foreach (var item in targetIncome.IncomeProducts)
                    {
                        IncomeProduct newIncomeProduct = new IncomeProduct()
                        {
                            Id = item.Id,
                            Name = item.Product.Name,
                            Description = item.Product.Description ?? string.Empty,
                            Type = item.Product.TypeFeature?.TypeFeature ?? string.Empty,
                            Amount = item.Amount
                        };
                        commandAnswer.IncomeProducts.Add(newIncomeProduct);
                    }

                    return new MasterMessage { Command = "income_products_found", CommandData = JsonSerializer.Serialize(commandAnswer) };
                }
            }
        }

        public MasterMessage ReturnEmptyMessage()
        {
            return new MasterMessage { Command = "income_products_found", CommandData = JsonSerializer.Serialize(new CommandAnswer()) };
        }

        private class CommandAnswer
        {
            public List<IncomeProduct> IncomeProducts { get; set; } = new();
        }
        private class IncomeProduct
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string Type { get; set; } = string.Empty;
            public double Amount { get; set; }
            public decimal Price { get; set; }
        }
    }
}
