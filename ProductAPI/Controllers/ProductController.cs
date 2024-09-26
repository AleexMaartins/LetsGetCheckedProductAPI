using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductAPI.Models.Entities;
using ProductAPI.Services;
using System.Collections.Generic;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet(Name = "GetProducts")]
        public IEnumerable<Product> Get()
        {
            return _productService.GetAllProducts();
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public Product Get(string id)
        {
            return _productService.GetProductById(id);
        }

    }
}