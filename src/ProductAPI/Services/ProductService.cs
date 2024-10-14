using ProductAPI.Data.Repositories;
using ProductAPI.Models;

namespace ProductAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Product> CreateProductAsync(CreateUpdateProductRequest createProductRequest)
        {
            return await _productRepository.CreateProductAsync(createProductRequest);
        }
        public async Task<IEnumerable<Product>> ReadAllProductsAsync()
        {
            return await _productRepository.ReadAllProductsAsync();
        }
        public async Task<Product?> ReadProductByIdAsync(Guid id)
        {
            return await _productRepository.ReadProductByIdAsync(id);
        }
        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateProductAsync(product);
        }
        public async Task DeleteProductByIdAsync(Guid id)
        {
            await _productRepository.DeleteProductByIdAsync(id);
        }
    }
}