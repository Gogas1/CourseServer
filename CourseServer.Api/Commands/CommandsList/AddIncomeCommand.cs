using CourseServer.Core.Interfaces.Services;
using CourseServer.Core.Models;
using System.Text.Json;

namespace CourseServer.Api.Commands.CommandsList
{
    public class AddIncomeCommand : Command
    {
        private readonly IProductsService _productsService;
        private readonly IIncomeService _incomeService;

        public AddIncomeCommand(IProductsService productsService, IIncomeService incomeService)
        {
            _productsService = productsService;
            _incomeService = incomeService;
        }

        public override async Task<MasterMessage> Execute(string content)
        {
            AddIncomeCommandData data = JsonSerializer.Deserialize<AddIncomeCommandData>(content);

            if (data == null)
            {
                return new MasterMessage { Command = "addincome_command_result", CommandData = JsonSerializer.Serialize(new CommandAnswer(true)) };
            }
            else
            {
                Income income = new Income()
                {
                    CreatedAt = DateTime.Now,
                    Supplier = data.Supplier,
                };

                foreach (var incomingProduct in data.Products)
                {
                    var product = ConvertCommandIncomeProductToProduct(incomingProduct);
                    product = await _productsService.CreateNewOrReturnExisting(product);

                    ProductIncome productIncome = new ProductIncome()
                    {
                        Price = incomingProduct.Price,
                        Product = product,
                        Amount = incomingProduct.Count
                    };

                    income.IncomeProducts.Add(productIncome);
                }

                income.Summ = income.IncomeProducts.Sum(x => x.Price * (decimal)x.Amount);
                await _incomeService.AddIncome(income);

                return new MasterMessage { Command = "addincome_command_result", CommandData = JsonSerializer.Serialize(new CommandAnswer(true)) };
            }
        }

        private Product ConvertCommandIncomeProductToProduct(CommandIncomeProduct commandIncomeProduct)
        {
            return new Product()
            {
                Id = commandIncomeProduct.Id,
                Name = commandIncomeProduct.Name,
                Description = commandIncomeProduct.Description,
                Amount = 0,
                TypeFeature = new ProductTypeFeature()
                {
                    TypeFeature = commandIncomeProduct.Type
                }
            };
        }

        private class CommandAnswer
        {
            public bool Success { get; set; }

            public CommandAnswer(bool success)
            {
                Success = success;
            }
        }
    }

    public class AddIncomeCommandData
    {
        public DateTime DateTime { get; set; }
        public string Supplier { get; set; } = string.Empty;
        public List<CommandIncomeProduct> Products { get; set; } = new();
    }

    public class CommandIncomeProduct
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Count { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}