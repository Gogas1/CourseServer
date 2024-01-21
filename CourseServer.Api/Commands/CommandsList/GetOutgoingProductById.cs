using CourseServer.Core.Interfaces.Services;
using System.Text.Json;

namespace CourseServer.Api.Commands.CommandsList
{
    public class GetOutgoingProductById : Command
    {
        private readonly IProductsService _productsService;

        public GetOutgoingProductById(IProductsService productsService)
        {
            _productsService = productsService;
        }

        public override async Task<MasterMessage> Execute(string content)
        {
            int id = JsonSerializer.Deserialize<int>(content);

            if (id == 0)
            {
                return ReturnFailtureMasterMassage();
            }
            else
            {
                var targetProduct = await _productsService.GetById(id);
                if (targetProduct == null)
                {
                    return ReturnFailtureMasterMassage();
                }
                else
                {
                    OugoingProduct ougoingProduct = new OugoingProduct
                    {
                        Id = targetProduct.Id,
                        Name = targetProduct.Name,
                        Description = targetProduct.Description ?? string.Empty,
                        Amount = targetProduct.Amount,
                        Price = targetProduct.PricingFeature.Price,
                        Type = targetProduct.TypeFeature.TypeFeature
                    };
                    return new MasterMessage { Command = "outgoing_product_search_result", CommandData = JsonSerializer.Serialize(ougoingProduct) };
                }
            }
        }

        private MasterMessage ReturnFailtureMasterMassage()
        {
            return new MasterMessage
            {
                Command = "outgoing_product_search_result",
                CommandData = JsonSerializer.Serialize(new CommandAnswer { Success = false })
            };
        }

        private class CommandAnswer
        {
            public bool Success { get; set; }
            public OugoingProduct FoundProduct { get; set; } = new();
        }

        private class OugoingProduct
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
