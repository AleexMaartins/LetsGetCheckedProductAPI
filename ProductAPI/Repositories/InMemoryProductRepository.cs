using ProductAPI.Models.Entities;
using System;
using System.Collections.Generic;

namespace ProductAPI.Repositories
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

        public IEnumerable<Product> GetAllProducts()
        {
            return Products;
        }
    }
}