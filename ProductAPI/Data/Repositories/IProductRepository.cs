using ProductAPI.Models.Entities;
using System.Collections.Generic;

namespace ProductAPI.Data.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(Guid id);
    }
}