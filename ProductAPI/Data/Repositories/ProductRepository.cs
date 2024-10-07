using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using ProductAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IAmazonDynamoDB _dynamoDbClient;
        private readonly Table _productTable;

        public ProductRepository(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
            _productTable = Table.LoadTable(_dynamoDbClient, "Products"); // Ensure this matches your table name
        }

        public async Task<IEnumerable<Product>> ReadAllProductsAsync()
        {
            var search = _productTable.Scan(new ScanFilter());
            var documentList = await search.GetNextSetAsync();
            return documentList.Select(doc => ConvertDocumentToProduct(doc));
        }

        public async Task<Product?> ReadProductByIdAsync(Guid id)
        {
            var document = await _productTable.GetItemAsync(id.ToString());
            return document != null ? ConvertDocumentToProduct(document) : null;
        }

        public async Task DeleteProductByIdAsync(Guid id)
        {
            await _productTable.DeleteItemAsync(id.ToString());
        }

        public async Task AddProductAsync(Product product)
        {
            var document = ConvertProductToDocument(product);
            await _productTable.PutItemAsync(document);
        }

        public async Task UpdateProductAsync(Product product)
        {
            var document = ConvertProductToDocument(product);
            await _productTable.UpdateItemAsync(document);
        }

        // Helper method to convert a DynamoDB document to a Product object.
        private Product ConvertDocumentToProduct(Document document)
        {
            return new Product
            {
                Id = Guid.Parse(document["Id"]),
                Name = document["Name"],
                Price = decimal.Parse(document["Price"]),
                Description = document["Description"],
                Stock = int.Parse(document["Stock"])
            };
        }

        // Helper method to convert a Product object to a DynamoDB document.
        private Document ConvertProductToDocument(Product product)
        {
            var document = new Document();
            document["Id"] = product.Id.ToString();
            document["Name"] = product.Name;
            document["Price"] = product.Price;
            document["Description"] = product.Description;
            document["Stock"] = product.Stock;
            return document;
        }
    }
}
