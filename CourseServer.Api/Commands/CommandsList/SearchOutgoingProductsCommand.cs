using CourseServer.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourseServer.Api.Commands.CommandsList
{
    public class SearchOutgoingProductsCommand : Command
    {
        private readonly IProductsService _productsService;

        public SearchOutgoingProductsCommand(IProductsService productsService)
        {
            _productsService = productsService;
        }

        public override async Task<MasterMessage> Execute(string content)
        {
            CommandData data = JsonSerializer.Deserialize<CommandData>(content ?? string.Empty) ?? new CommandData();

            var products = await _productsService.GetListByIdAndName(data.Id, data.Name);
            CommandAnswer commandAnswer = new CommandAnswer();
            foreach (var item in products)
            {
                ProductRecord newProductRecord = new ProductRecord
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description ?? string.Empty,
                    Amount = item.Amount,
                    Price = item.PricingFeature?.Price ?? 0,
                    Type = item.TypeFeature?.TypeFeature ?? string.Empty
                };
                commandAnswer.FoundProducts.Add(newProductRecord);
            }

            return new MasterMessage { Command = "outgoing_products_search_result", CommandData = JsonSerializer.Serialize(commandAnswer) };
        }

        private class CommandData
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        private class CommandAnswer
        {
            public List<ProductRecord> FoundProducts { get; set; } = new();
        }

        private class ProductRecord
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
