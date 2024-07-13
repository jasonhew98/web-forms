using Core.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
    }
}