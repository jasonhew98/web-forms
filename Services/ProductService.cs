using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebForms.Models;
using WebForms.Services.Interface;

namespace WebForms.Services
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