using ProductsAPI.Entities;

namespace ProductsAPI.Services;

public interface ICategoriesService
{
    Task<List<Category>> FindAllCategoriesAsync();

    Task<Category> FindCategoryByIdAsync(int id);

    Task<Category> CreateCategoryAsync(Category category);

    Task<Category> UpdateCategoryAsync(int id, Category category);

    Task DeleteCategoryAsync(int id);
}