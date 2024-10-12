using ProductAPI.Models;

namespace ProductAPI.Data.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> ReadAllProductsAsync();
        Task<Product?> ReadProductByIdAsync(Guid id);
        Task DeleteProductByIdAsync(Guid id);
        Task AddProductAsync(CreateProductRequest createProductRequest);
        Task UpdateProductAsync(Product product);
    }
}