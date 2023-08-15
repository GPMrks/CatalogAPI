using CatalogAPI.DTOs;
using CatalogAPI.Entities;

namespace CatalogAPI.Services;

public interface ICategoriesService
{
    Task<List<CategoryDTO>> FindAllCategoriesAsync();

    Task<List<CategoryDTO>> FindProductsInCategories();
    
    Task<CategoryDTO> FindCategoryByIdAsync(int id);

    Task<CategoryDTO> CreateCategoryAsync(CategoryForm categoryForm);

    Task<CategoryDTO> UpdateCategoryAsync(int id, CategoryForm categoryForm);
    
    Task<CategoryDTO> UpdateCategoryPatchAsync(int id, CategoryFormPatch categoryFormPatch);

    Task DeleteCategoryAsync(int id);
}