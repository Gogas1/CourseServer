using CourseServer.Core.Interfaces.Services;
using System.Text.Json;

namespace CourseServer.Api.Commands.CommandsList
{
    public class UpdateProductFeaturesCommand : Command
    {
        private IProductsService _productsService;

        public UpdateProductFeaturesCommand(IProductsService productsService)
        {
            _productsService = productsService;
        }

        public override async Task<MasterMessage> Execute(string content)
        {
            CommandData data = JsonSerializer.Deserialize<CommandData>(content ?? string.Empty) ?? new CommandData();

            if (data.Id == 0)
            {
                return new MasterMessage { Command = "update_product_features", CommandData = JsonSerializer.Serialize("failture") };
            }
            else
            {
                await _productsService.UpdateFeatures(data.Id, data.Type, data.Price);
                return new MasterMessage { Command = "update_product_features", CommandData = JsonSerializer.Serialize("success") };
            }
        }

        private class CommandData
        {
            public int Id { get; set; }
            public string Type { get; set; } = string.Empty;
            public decimal Price { get; set; }
        }
    }
}
