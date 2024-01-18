using CourseServer.Core.Interfaces.Services;
using CourseServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourseServer.Api.Commands.CommandsList
{
    public class AddIncomeProductSearchCommand : Command
    {
        private readonly IProductsService _productsService;

        public AddIncomeProductSearchCommand(IProductsService productsService)
        {
            _productsService = productsService;
        }

        public override async Task<MasterMessage> Execute(string content)
        {
            CommandData? data = JsonSerializer.Deserialize<CommandData>(content);

            if(data == null)
            {
                return new MasterMessage { Command = "incomeproductsearch_wrongparams", CommandData = "wrongformat" };
            }
            else
            {
                if(data.Id != 0)
                {
                    var targetProduct = await _productsService.GetById(data.Id);
                    if(targetProduct != null)
                    {
                        ResponseData responseData = new();
                        responseData.ProductFound = true;
                        responseData.FoundProducts.Add(new ProductFound(targetProduct.Id,
                                                                        targetProduct.Name,
                                                                        targetProduct.Description ?? string.Empty,
                                                                        targetProduct.TypeFeature?.TypeFeature ?? string.Empty ));

                        return new MasterMessage { Command = "addincome_productfound", CommandData = JsonSerializer.Serialize(responseData) };
                    }
                    else
                    {
                        return await GetMasterMessageByNameSearch(data.Name);
                    }                    
                }
                else
                {
                    return await GetMasterMessageByNameSearch(data.Name);
                }
            }
        }

        /// <summary>
        /// Returns MasterMessage with successfull result if any product has found by specified name. Returns MasterMessage with failture result if no products have found
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private async Task<MasterMessage> GetMasterMessageByNameSearch(string name)
        {
            if(!string.IsNullOrEmpty(name))
            {
                var targetProducts = await _productsService.GetListByName(name);
                if (targetProducts.Any())
                {
                    ResponseData responseData = new();
                    responseData.ProductFound = true;
                    foreach (var product in targetProducts)
                    {
                        responseData.FoundProducts.Add(new ProductFound(product.Id,
                                                                        product.Name,
                                                                        product.Description ?? string.Empty,
                                                                        product.TypeFeature?.TypeFeature ?? string.Empty));
                    }

                    return new MasterMessage { Command = "addincome_productfound", CommandData = JsonSerializer.Serialize(responseData) };
                }
            }
            
            ResponseData failedResponseData = new();
            failedResponseData.ProductFound = false;

            return new MasterMessage { Command = "addincome_productfound", CommandData = JsonSerializer.Serialize(failedResponseData) };
        }

        private class CommandData
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        private class ResponseData
        {
            public bool ProductFound { get; set; }
            public List<ProductFound> FoundProducts { get; set; } = new();
        }

        private class ProductFound
        {
            public ProductFound(int id, string name, string description, string type)
            {
                Id = id;
                Name = name;
                Description = description;
                Type = type;
            }

            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string Type { get; set; } = string.Empty;
        }
    }
}
