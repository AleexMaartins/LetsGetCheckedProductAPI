using ProductAPI.Data.Repositories;
using ProductAPI.Models.Entities;
using System.Collections.Generic;

namespace ProductAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> ReadAllProducts()
        {
            return _productRepository.ReadAllProducts();
        }
        public Product? ReadProductById(Guid id)
        {
            return _productRepository.ReadProductById(id);
        }
        public void DeleteProductById(Guid id)
        {
            _productRepository.DeleteProductById(id);
        }
        public void AddProduct(Product product)
        {
            _productRepository.AddProduct(product);
        }
        public void UpdateProduct(Product product)
        {
            _productRepository.UpdateProduct(product);
        }
    }
}