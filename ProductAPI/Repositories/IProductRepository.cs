using ProductAPI.Models.Entities;
using System.Collections.Generic;

namespace ProductAPI.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
    }
}