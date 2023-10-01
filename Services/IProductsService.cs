using CatalogAPI.DTOs;
using CatalogAPI.Entities;

namespace CatalogAPI.Services;

public interface IProductsService
{
    List<ProductDTO> FindAllProducts();

    List<ProductDTO> FindProductsSortedByPrice();
    
    ProductDTO FindProductById(int id);

    ProductDTO CreateProduct(ProductForm productForm);

    ProductDTO UpdateProduct(int id, ProductForm productForm);
    
    ProductDTO UpdateProductPatch(int id, ProductFormPatch productFormPatch);

    void DeleteProduct(int id);
}