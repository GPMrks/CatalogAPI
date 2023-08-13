using CatalogAPI.Entities;
using CatalogAPI.Entities.DTOs;

namespace CatalogAPI.Services;

public interface ICategoriesService
{
    Task<List<CategoryDTO>> FindAllCategoriesAsync();

    Task<List<CategoryDTO>> FindProductsInCategories();
    
    Task<CategoryDTO> FindCategoryByIdAsync(int id);

    Task<CategoryDTO> CreateCategoryAsync(CategoryForm categoryForm);

    Task<CategoryDTO> UpdateCategoryAsync(int id, CategoryForm categoryForm);

    Task DeleteCategoryAsync(int id);
}