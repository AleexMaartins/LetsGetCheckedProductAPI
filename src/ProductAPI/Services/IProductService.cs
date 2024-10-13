using ProductAPI.Models;

namespace ProductAPI.Services
{
    public interface IProductService
    {
        Task<Product> AddProductAsync(CreateUpdateProductRequest createProductRequest);
        Task<IEnumerable<Product>> ReadAllProductsAsync();
        Task<Product?> ReadProductByIdAsync(Guid id);
        Task UpdateProductAsync(Product product);
        Task DeleteProductByIdAsync(Guid id);
    }
}