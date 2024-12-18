﻿using CourseServer.Core.Models;

namespace CourseServer.Core.Interfaces.Services
{
    public interface IProductsService
    {
        Task<Product> CreateNewOrReturnExisting(Product product);
        Task<Product?> GetById(int id);
        Task<Product> Add(Product product);
        Task<List<Product>> GetListByIds(IEnumerable<int> ids);
        Task<List<Product>> GetListByName(string name);
        Task<List<Product>> GetListByNameAndType(string name, string type);
        Task<List<Product>> GetListByIdAndName(int id, string name);
        Task UpdateFeatures(int productId, string newType, decimal newPrice);
    }
}
