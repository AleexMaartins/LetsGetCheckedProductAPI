using ProductAPI.Models.Entities;
using System.Collections.Generic;

namespace ProductAPI.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(Guid id);
    }
}