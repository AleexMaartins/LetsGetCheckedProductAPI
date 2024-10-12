using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using ProductAPI.Models;

namespace ProductAPI.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IAmazonDynamoDB _dynamoDbClient;
        private const string TableName = "Products"; // DynamoDB table name

        public ProductRepository(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
        }
        public async Task AddProductAsync(Product product)
        {
            product.Id = Guid.NewGuid();
            var request = new PutItemRequest
            {
                TableName = TableName,
                Item = new Dictionary<string, AttributeValue>
                {
                    {"ProductId", new AttributeValue { S = product.Id.ToString() }},
                    {"Name", new AttributeValue { S = product.Name }},
                    {"Price", new AttributeValue { N = product.Price.ToString() }},
                    {"Description", new AttributeValue { S = product.Description }},
                    {"Stock", new AttributeValue { N = product.Stock.ToString() }},
                }
            };
            await _dynamoDbClient.PutItemAsync(request);
        }
        public async Task<IEnumerable<Product>> ReadAllProductsAsync()
        {
            var scanRequest = new ScanRequest
            {
                TableName = TableName
            };
            var response = await _dynamoDbClient.ScanAsync(scanRequest);
            var products = new List<Product>();

            foreach (var item in response.Items)
            {
                products.Add(new Product
                {
                    Id = Guid.Parse(item["ProductId"].S),
                    Name = item["Name"].S,
                    Price = decimal.Parse(item["Price"].N),
                    Description = item["Description"].S,
                    Stock = int.Parse(item["Stock"].N)
                });
            }

            return products;
        }
        public async Task<Product?> ReadProductByIdAsync(Guid id)
        {
            var request = new GetItemRequest
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "ProductId", new AttributeValue { S = id.ToString() } }
                }
            };
            var response = await _dynamoDbClient.GetItemAsync(request);

            if (response.Item == null || response.Item.Count == 0)
            {
                return null;
            }

            return new Product
            {
                Id = Guid.Parse(response.Item["ProductId"].S),
                Name = response.Item["Name"].S,
                Price = decimal.Parse(response.Item["Price"].N),
                Description = response.Item["Description"].S,
                Stock = int.Parse(response.Item["Stock"].N)
            };
        }
        public async Task DeleteProductByIdAsync(Guid id)
        {
            var request = new DeleteItemRequest
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "ProductId", new AttributeValue { S = id.ToString() } }
                }
            };
            await _dynamoDbClient.DeleteItemAsync(request);
        }
        public async Task UpdateProductAsync(Product product)
        {
            var request = new UpdateItemRequest
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "ProductId", new AttributeValue { S = product.Id.ToString() } }
                },
                AttributeUpdates = new Dictionary<string, AttributeValueUpdate>
                {
                    { "Name", new AttributeValueUpdate { Action = "PUT", Value = new AttributeValue { S = product.Name } } },
                    { "Price", new AttributeValueUpdate { Action = "PUT", Value = new AttributeValue { N = product.Price.ToString() } } },
                    { "Description", new AttributeValueUpdate { Action = "PUT", Value = new AttributeValue { S = product.Description } } },
                    { "Stock", new AttributeValueUpdate { Action = "PUT", Value = new AttributeValue { N = product.Stock.ToString() } } },
                }
            };
            await _dynamoDbClient.UpdateItemAsync(request);
        }
    }
}
