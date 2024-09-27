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

        [HttpGet(Name = "readProducts")]
        public ActionResult<IEnumerable<Product>> Read()
        {
            var products = _productService.ReadAllProducts();
            return Ok(products);
        }

        [HttpGet("{id:guid}", Name = "readProduct")]
        public ActionResult<Product> Read(Guid id)
        {
            var product = _productService.ReadProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpDelete("{id:guid}", Name = "deleteProduct")]
        public IActionResult Delete(Guid id)
        {
            var product = _productService.ReadProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            _productService.DeleteProductById(id);
            return NoContent();
        }

        [HttpPost(Name = "addProduct")]
        public IActionResult Add([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product cannot be null.");
            }

            _productService.AddProduct(product);
            return CreatedAtRoute("readProduct", new { id = product.Id }, product);
        }

        [HttpPut("{id:guid}", Name = "updateProduct")]
        public IActionResult Update(Guid id, [FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product cannot be null.");
            }

            var existingProduct = _productService.ReadProductById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            _productService.UpdateProduct(product);
            return NoContent();
        }
    }
}