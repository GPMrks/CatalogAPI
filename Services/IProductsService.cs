using CatalogAPI.Entities;

namespace CatalogAPI.Services;

public interface IProductsService
{
    Task<List<Product>> FindAllProductsAsync();

    Task<Product> FindProductByIdAsync(int id);

    Task<Product> CreateProductAsync(Product product);

    Task<Product> UpdateProductAsync(int id, Product product);

    Task DeleteProductAsync(int id);
}