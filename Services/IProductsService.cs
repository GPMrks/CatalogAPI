using CatalogAPI.DTOs;
using CatalogAPI.Entities;

namespace CatalogAPI.Services;

public interface IProductsService
{
    Task<List<ProductDTO>> FindAllProductsAsync();

    Task<ProductDTO> FindProductByIdAsync(int id);

    Task<ProductDTO> CreateProductAsync(ProductForm productForm);

    Task<ProductDTO> UpdateProductAsync(int id, ProductForm productForm);

    Task DeleteProductAsync(int id);
}