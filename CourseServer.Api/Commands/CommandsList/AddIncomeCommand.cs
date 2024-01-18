using CourseServer.Core.Interfaces.Services;
using CourseServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
                return new MasterMessage { Command = "addincome_wrongparams", CommandData = "wrongformat" };
            }
            else
            {
                Income income = new Income()
                {
                    DateTime = DateTime.Now,
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

                return new MasterMessage { Command = "addincome_succeeded", CommandData = "success" };
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
    }

    public class AddIncomeCommandData
    {
        public DateTime DateTime { get; set; }
        public string Supplier { get; set; }
        public List<CommandIncomeProduct> Products { get; set; } = new();
    }

    public class CommandIncomeProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Count { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
    }
}