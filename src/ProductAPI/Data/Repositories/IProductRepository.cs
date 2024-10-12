using ProductAPI.Models;

namespace ProductAPI.Data.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> ReadAllProductsAsync();
        Task<Product?> ReadProductByIdAsync(Guid id);
        Task DeleteProductByIdAsync(Guid id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
    }
}