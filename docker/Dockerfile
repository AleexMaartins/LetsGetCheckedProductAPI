FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY src/ProductAPI/ProductAPI.csproj src/ProductAPI/
RUN dotnet restore src/ProductAPI/ProductAPI.csproj

COPY src/ src/

RUN dotnet build src/ProductAPI/ProductAPI.csproj -c Release -o /app/build

RUN dotnet publish src/ProductAPI/ProductAPI.csproj -c Release -o /app/publish

RUN dotnet restore src/ProductAPI/Tests/ProductAPI.Tests/ProductAPI.Tests.csproj

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

# Start the application
ENTRYPOINT ["dotnet", "ProductAPI.dll"]