using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using ProductAPI.Configuration;
using ProductAPI.Data.Repositories;
using ProductAPI.Services;
var builder = WebApplication.CreateBuilder(args);


// Configure AWS DynamoDB
builder.Services.AddAWSService<IAmazonDynamoDB>();

// Bind AWS Config from appsettings.json
var awsOptions = builder.Configuration.GetSection("AwsConfig").Get<AwsConfig>();

// Create AWS credentials using the provided keys
var credentials = new BasicAWSCredentials(awsOptions.AccessKey, awsOptions.SecretKey);

// Set up the DynamoDB client with these credentials and configuration
var dynamoDbClient = new AmazonDynamoDBClient(credentials, new AmazonDynamoDBConfig
{
    ServiceURL = awsOptions.ServiceUrl,
    RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(awsOptions.Region),
    UseHttp = true,    // Ensure you're using HTTP instead of HTTPS for local development
});

// Register DynamoDB client in the service collection
builder.Services.AddSingleton<IAmazonDynamoDB>(dynamoDbClient);



// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();