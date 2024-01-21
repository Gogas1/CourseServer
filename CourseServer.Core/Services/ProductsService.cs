using CourseServer.Core.Interfaces.Repos;
using CourseServer.Core.Interfaces.Services;
using CourseServer.Core.Models;

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

        public async Task<Product> CreateNewOrReturnExisting(Product product)
        {
            if (product.Id == 0)
            {
                return await _productsRepo.AddProductAsync(product);
            }
            else
            {
                var targetProduct = await _productsRepo.GetProductByIdAsync(product.Id);
                if (targetProduct == null)
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

        public async Task<List<Product>> GetListByIdAndName(int id, string name)
        {
            if (id != 0)
            {
                var products = await _productsRepo.GetProductsByConditionAsync(_ => _.Id == id);
                return products.ToList();
            }
            else
            {
                var products = await _productsRepo.GetProductsByConditionAsync(_ => _.Name.Contains(name));
                return products.ToList();
            }

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

        public async Task<List<Product>> GetListByNameAndType(string name, string type)
        {
            var products = await _productsRepo.GetProductsByConditionAsync(_ => _.Name.Contains(name) && _.TypeFeature.TypeFeature.Contains(type));

            return products.ToList();
        }

        public async Task UpdateFeatures(int productId, string newType, decimal newPrice)
        {
            var targetProduct = await GetById(productId);
            if (targetProduct == null) return;

            if (targetProduct.PricingFeature == null)
            {
                var pricingFeature = new ProductPricingFeature();
                pricingFeature.Price = newPrice;
                targetProduct.PricingFeature = pricingFeature;
            }
            else
            {
                targetProduct.PricingFeature.Price = newPrice;
            }

            if (targetProduct.TypeFeature == null)
            {
                var typeFeature = new ProductTypeFeature();
                typeFeature.TypeFeature = newType;
                targetProduct.TypeFeature = typeFeature;
            }
            else
            {
                targetProduct.TypeFeature.TypeFeature = newType;
            }

            await _productsRepo.UpdateAsync(targetProduct);
        }
    }
}
