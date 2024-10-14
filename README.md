# ProductAPI

## Overview

ProductAPI is a simple RESTful API designed to manage products in an inventory system. It allows users to create, read, update, and delete products. This API is built using .NET Core and integrates with DynamoDB for data storage, running on LocalStack.

## Features

- Create new products
- Retrieve product details by ID
- Update existing products
- Delete products from the inventory
- Comprehensive error handling and validation
- Unit tests to ensure code quality
- Swagger documentation for API endpoints

## Technologies Used

- **.NET Core (version 8)**
- **ASP.NET Core**
- **Entity Framework Core**
- **DynamoDB** (using LocalStack)
- **Docker**
- **Moq** (for unit testing)
- **MSTest** (for unit testing)
- **FluentValidation** (for model validation)
- **Swagger/OpenAPI** (for API documentation)

## Prerequisites

Before you begin, ensure you have met the following requirements:

- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/)

## Getting Started

### Clone the Repository

To clone the repository and navigate into the project directory, run:

```bash
git clone https://github.com/AleexMaartins/LetsGetCheckedProductAPI
cd .\LetsGetCheckedProductAPI\docker\
docker compose up --build
```
### Accessing the API
http://localhost:5045/swagger/index.html
