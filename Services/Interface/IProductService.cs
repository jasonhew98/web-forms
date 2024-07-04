using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebForms.Models;

namespace WebForms.Services.Interface
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
    }
}