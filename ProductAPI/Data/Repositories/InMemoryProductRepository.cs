using ProductAPI.Models.Entities;
using System;
using System.Collections.Generic;

namespace ProductAPI.Data.Repositories
{
    public class InMemoryProductRepository : IProductRepository
    {
        private static readonly List<Product> Products = new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Keyboard",
                Price = 20,
                Description = "A keyboard",
                Stock = 100
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Mouse",
                Price = 10,
                Description = "A mouse",
                Stock = 50
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Monitor",
                Price = 100,
                Description = "A monitor",
                Stock = 30
            }
        };

        public IEnumerable<Product> ReadAllProducts()
        {
            return Products;
        }
        public Product? ReadProductById(Guid id)
        {
            return Products.Find(p => p.Id == id);
        }

        public void DeleteProductById(Guid id)
        {
            var product = Products.Find(p => p.Id == id);
            if (product != null)
            {
                Products.Remove(product);
            }
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = Products.Find(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                existingProduct.Stock = product.Stock;
            }
        }
    }
}