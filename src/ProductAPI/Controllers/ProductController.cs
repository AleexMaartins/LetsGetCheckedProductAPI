﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductAPI.Models;
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

        [HttpPost(Name = "createProduct")]
        public async Task<ActionResult<Product>> Create([FromBody] CreateUpdateProductRequest product)
        {
            if (product == null)
            {
                return BadRequest("Product cannot be null.");
            }

            var newProduct = await _productService.CreateProductAsync(product);
            return Ok(newProduct);

        }

        [HttpGet(Name = "readProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> Read()
        {
            var products = await _productService.ReadAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id:guid}", Name = "readProduct")]
        public async Task<ActionResult<Product>> Read(Guid id)
        {
            var product = await _productService.ReadProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPut("{id:guid}", Name = "updateProduct")]
        public async Task<ActionResult<Product>> Update(Guid id, [FromBody] CreateUpdateProductRequest updateProductRequest)
        {
            if (updateProductRequest == null)
            {
                return BadRequest("Product cannot be null.");
            }

            var existingProduct = await _productService.ReadProductByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            var product = new Product
            {
                Id = id,
                Name = updateProductRequest.Name,
                Price = updateProductRequest.Price,
                Description = updateProductRequest.Description,
                Stock = updateProductRequest.Stock
            };
            await _productService.UpdateProductAsync(product);
            return Ok(product);
        }

        [HttpDelete("{id:guid}", Name = "deleteProduct")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productService.ReadProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productService.DeleteProductByIdAsync(id);
            return NoContent();
        }
    }
}