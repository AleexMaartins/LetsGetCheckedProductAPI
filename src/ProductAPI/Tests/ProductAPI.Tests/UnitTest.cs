using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductAPI.Controllers;
using ProductAPI.Models;
using ProductAPI.Services;

namespace ProductAPI.Tests
{
    [TestClass]
    public class ProductsControllerTests
    {
        private ProductController _controller;
        private Mock<IProductService> _mockProductService;
        private Mock<ILogger<ProductController>> _mockLogger;

        [TestInitialize]
        public void Setup()
        {
            _mockProductService = new Mock<IProductService>();
            _mockLogger = new Mock<ILogger<ProductController>>();
            _controller = new ProductController(_mockLogger.Object, _mockProductService.Object);
        }

        [TestMethod]
        public async Task Create_ValidProduct_ReturnsOkResult()
        {
            var newProductRequest = new CreateUpdateProductRequest
            {
                Name = "New Product",
                Price = 50.0M,
                Description = "New Product Description",
                Stock = 10
            };

            var createdProduct = new Product
            {
                Id = Guid.NewGuid(),
                Name = newProductRequest.Name,
                Price = newProductRequest.Price,
                Description = newProductRequest.Description,
                Stock = newProductRequest.Stock
            };

            _mockProductService.Setup(service => service.CreateProductAsync(newProductRequest))
                               .ReturnsAsync(createdProduct);

            var result = await _controller.Create(newProductRequest);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(createdProduct, okResult.Value);

        }

        [TestMethod]
        public async Task Create_NullProduct_ReturnsBadRequest()
        {
            var result = await _controller.Create(null);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }
        [TestMethod]
        public async Task ReadProductById_ValidId_ReturnsOkResult()
        {

            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, Name = "Test Product", Price = 100.0M, Description = "Test Description" };

            _mockProductService.Setup(service => service.ReadProductByIdAsync(productId))
                               .ReturnsAsync(product);

            var result = await _controller.Read(productId);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(product, okResult.Value);
        }

        [TestMethod]
        public async Task ReadProductById_InvalidId_ReturnsNotFoundResult()
        {
            var productId = Guid.NewGuid(); // non-existent ID
            _mockProductService.Setup(service => service.ReadProductByIdAsync(productId))
                               .ReturnsAsync((Product)null); // simulate no product found

            var result = await _controller.Read(productId);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }



        [TestMethod]
        public async Task Update_ValidId_ReturnsOkResult()
        {
            var productId = Guid.NewGuid();
            var existingProduct = new Product { Id = productId, Name = "Existing Product", Price = 100.0M, Description = "Existing Description", Stock = 10 };

            _mockProductService.Setup(service => service.ReadProductByIdAsync(productId))
                               .ReturnsAsync(existingProduct);

            var updateProductRequest = new CreateUpdateProductRequest
            {
                Name = "Updated Product",
                Price = 75.0M,
                Description = "Updated Description",
                Stock = 5
            };

            var updatedProduct = new Product
            {
                Id = productId,
                Name = updateProductRequest.Name,
                Price = updateProductRequest.Price,
                Description = updateProductRequest.Description,
                Stock = updateProductRequest.Stock
            };

            _mockProductService.Setup(service => service.ReadProductByIdAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(existingProduct);


            var result = await _controller.Update(productId, updateProductRequest);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedProduct = okResult.Value as Product;
            Assert.IsNotNull(returnedProduct);
            Assert.AreEqual(existingProduct.Id, returnedProduct.Id);
            Assert.AreEqual(updatedProduct.Name, returnedProduct.Name);
            Assert.AreEqual(updatedProduct.Price, returnedProduct.Price);
            Assert.AreEqual(updatedProduct.Description, returnedProduct.Description);
        }

        [TestMethod]
        public async Task Update_NullProduct_ReturnsBadRequest()
        {
            var productId = Guid.NewGuid();

            var result = await _controller.Update(productId, null);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task Delete_ValidId_ReturnsNoContent()
        {
            var productId = Guid.NewGuid();
            var existingProduct = new Product { Id = productId, Name = "Existing Product", Price = 100.0M, Description = "Existing Description", Stock = 10 };

            _mockProductService.Setup(service => service.ReadProductByIdAsync(productId))
                               .ReturnsAsync(existingProduct);

            var result = await _controller.Delete(productId);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task Delete_InvalidId_ReturnsNotFound()
        {
            var productId = Guid.NewGuid(); // non-existent ID
            _mockProductService.Setup(service => service.ReadProductByIdAsync(productId))
                               .ReturnsAsync((Product)null); // simulate no product found

            var result = await _controller.Delete(productId);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
