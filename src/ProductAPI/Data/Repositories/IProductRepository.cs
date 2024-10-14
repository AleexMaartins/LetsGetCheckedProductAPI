using ProductAPI.Models;

namespace ProductAPI.Data.Repositories
{
    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(CreateUpdateProductRequest createProductRequest);
        Task<IEnumerable<Product>> ReadAllProductsAsync();
        Task<Product?> ReadProductByIdAsync(Guid id);
        Task UpdateProductAsync(Product product);
        Task DeleteProductByIdAsync(Guid id);

    }
}