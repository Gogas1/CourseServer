using CourseServer.Core.Interfaces.Repos;
using CourseServer.Core.Interfaces.Services;
using CourseServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Core.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepo _productsRepo;

        public ProductsService(IProductsRepo productsRepo)
        {
            _productsRepo = productsRepo;
        }

        public async Task<Product> Add(Product product)
        {
            return await _productsRepo.AddProductAsync(product);
        }

        public async Task<Product?> CreateNewOrReturnExisting(Product product)
        {
            if(product.Id == 0)
            {
                return await _productsRepo.AddProductAsync(product);
            }
            else
            {              
                var targetProduct = await _productsRepo.GetProductByIdAsync(product.Id);
                if(targetProduct ==  null)
                {
                    product.Id = 0;
                    return await _productsRepo.AddProductAsync(product);
                }
                else
                {
                    return targetProduct;
                }
            }
        }

        public async Task<Product?> GetById(int id)
        {
            return await _productsRepo.GetProductByIdAsync(id);
        }

        public async Task<List<Product>> GetListByIds(IEnumerable<int> ids)
        {
            return await _productsRepo.GetProductsByIdsAsync(ids);
        }

        public async Task<List<Product>> GetListByName(string name)
        {
            var products = await _productsRepo.GetProductsByNameAsync(name);

            return products.ToList();
        }
    }
}
