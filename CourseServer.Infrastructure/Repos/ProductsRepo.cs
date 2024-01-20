using CourseServer.Core.Interfaces.Repos;
using CourseServer.Core.Models;
using CourseServer.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Infrastructure.Repos
{
    public class ProductsRepo : IProductsRepo
    {
        private readonly ProductsDbContext _context;

        public ProductsRepo(ProductsDbContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product?> GetProductByFullNameAsync(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product?> GetProductByPartNameAsync(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Name.Contains(name));
        }

        public async Task<IEnumerable<Product>> GetProductsByConditionAsync(Expression<Func<Product, bool>> condition)
        {
            return await _context.Products
                .Include(_ => _.TypeFeature)
                .Include(_ => _.PricingFeature)
                .Where(condition)
                .ToListAsync();
        }

        public async Task<List<Product>> GetProductsByIdsAsync(IEnumerable<int> ids)
        {
            return await _context.Products.Where(p => ids.Contains(p.Id)).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            return await _context.Products.Where(p => p.Name.Contains(name)).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByPartNameAsync(string name)
        {
            return await _context.Products
                .Include(_ => _.TypeFeature)
                .Include(_ => _.PricingFeature)
                .Where(_ => _.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByTypeAsync(string type)
        {
            return await _context.Products
                .Include(_ => _.TypeFeature)
                .Include(_ => _.PricingFeature)
                .Where(_ => _.TypeFeature.TypeFeature.Contains(type))
                .ToListAsync();
        }
    }
}
