using ProductAPI.Models.Entities;
using ProductAPI.Repositories;
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

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }
        public Product GetProductById(string id)
        {
            return _productRepository.GetProductById(id);
        }
    }
}