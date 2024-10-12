using ProductAPI.Models;

namespace ProductAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> ReadAllProductsAsync();
        Task<Product?> ReadProductByIdAsync(Guid id);
        Task DeleteProductByIdAsync(Guid id);
        Task AddProductAsync(CreateUpdateProductRequest createProductRequest);
        Task UpdateProductAsync(Product product);
    }
}