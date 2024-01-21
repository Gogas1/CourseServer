using CourseServer.Core.Models;
using System.Linq.Expressions;

namespace CourseServer.Core.Interfaces.Repos
{
    public interface IProductsRepo
    {
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product?> GetProductByFullNameAsync(string name);
        Task<Product?> GetProductByPartNameAsync(string name);
        Task<Product> AddProductAsync(Product product);
        Task<List<Product>> GetProductsByIdsAsync(IEnumerable<int> ids);
        Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
        Task<IEnumerable<Product>> GetProductsByConditionAsync(Expression<Func<Product, bool>> condition);
        Task<IEnumerable<Product>> GetProductsByPartNameAsync(string name);
        Task UpdateAsync(Product product);
    }
}
