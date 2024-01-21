using CourseServer.Core.Interfaces.Services;
using CourseServer.Core.Models;
using System.Text.Json;

namespace CourseServer.Api.Commands.CommandsList
{
    public class SubmitOutgoingCommand : Command
    {
        private readonly IOutgoingService _outgoingService;
        private readonly IProductsService _productsService;

        public SubmitOutgoingCommand(IOutgoingService outgoingService, IProductsService productsService)
        {
            _outgoingService = outgoingService;
            _productsService = productsService;
        }

        public override async Task<MasterMessage> Execute(string content)
        {
            CommandData data = JsonSerializer.Deserialize<CommandData>(content ?? string.Empty) ?? new CommandData();

            if (!data.Products.Any())
            {
                return ReturnFailtureMasterMessageResult();
            }
            else
            {
                Outgoing newOutgoing = new Outgoing
                {
                    DateTime = DateTime.Now,
                    Manager = data.Manager
                };
                foreach (var outgoingProduct in data.Products)
                {
                    var targetProduct = await _productsService.GetById(outgoingProduct.Id);
                    if (targetProduct == null)
                    {
                        return ReturnFailtureMasterMessageResult();
                    }

                    ProductOutgoing productOutgoing = new ProductOutgoing
                    {
                        Amount = outgoingProduct.Amount,
                        Price = outgoingProduct.Price,
                        Product = targetProduct
                    };

                    newOutgoing.OutgoingProducts.Add(productOutgoing);
                    await _outgoingService.Add(newOutgoing);

                    return new MasterMessage { Command = "outgoing_submit_result", CommandData = JsonSerializer.Serialize(new CommandAnswer(true)) };
                }
            }

            return ReturnFailtureMasterMessageResult();
        }

        private MasterMessage ReturnFailtureMasterMessageResult()
        {
            return new MasterMessage { Command = "outgoing_submit_result", CommandData = JsonSerializer.Serialize(new CommandAnswer(false)) };
        }

        private class CommandData
        {
            public string Manager { get; set; } = string.Empty;
            public List<OutgoingProduct> Products { get; set; } = new();
        }

        private class CommandAnswer
        {
            public bool Success { get; set; }

            public CommandAnswer(bool success)
            {
                Success = success;
            }
        }

        private class OutgoingProduct
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public double Amount { get; set; }
            public decimal Price { get; set; }
            public string Type { get; set; } = string.Empty;
        }
    }
}
