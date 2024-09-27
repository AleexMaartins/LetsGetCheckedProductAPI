using ProductAPI.Models.Entities;
using System.Collections.Generic;

namespace ProductAPI.Data.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> ReadAllProducts();
        Product? ReadProductById(Guid id);
        void DeleteProductById(Guid id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
    }
}