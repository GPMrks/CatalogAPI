using CatalogAPI.DTOs;
using CatalogAPI.Entities;
using CatalogAPI.Pagination;

namespace CatalogAPI.Services;

public interface IProductsService
{
    List<ProductDTO> GetAllProducts(ProductsParameters productsParameters);

    List<ProductDTO> GetProductsSortedByPrice();
    
    ProductDTO GetProductById(int id);

    ProductDTO CreateProduct(ProductForm productForm);

    ProductDTO UpdateProduct(int id, ProductForm productForm);
    
    ProductDTO UpdateProductPatch(int id, ProductFormPatch productFormPatch);

    void DeleteProduct(int id);
}