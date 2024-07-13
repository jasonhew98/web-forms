using Core.Services.Interfaces;
using Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ProductService : IProductService
    {
        public ProductService() { }

        public async Task<List<Product>> GetProducts()
        {
            return new List<Product>() { new Product { ProductId = Guid.NewGuid().ToString() } };
        }
    }
}