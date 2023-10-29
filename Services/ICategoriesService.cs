using CatalogAPI.DTOs;
using CatalogAPI.Entities;
using CatalogAPI.Pagination;

namespace CatalogAPI.Services;

public interface ICategoriesService
{
    List<CategoryDTO> GetAllCategories(CategoriesParameters categoriesParameters);

    List<CategoryDTO> GetProductsInCategories();
    
    CategoryDTO GetCategoryById(int id);

    CategoryDTO CreateCategory(CategoryForm categoryForm);

    CategoryDTO UpdateCategory(int id, CategoryForm categoryForm);
    
    CategoryDTO UpdateCategoryPatch(int id, CategoryFormPatch categoryFormPatch);

    void DeleteCategory(int id);
}