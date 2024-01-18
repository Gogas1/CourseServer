﻿using CourseServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}