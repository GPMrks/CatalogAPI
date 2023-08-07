using Microsoft.EntityFrameworkCore;
using ProductsAPI.Context;
using ProductsAPI.Entities;
using ProductsAPI.Exceptions;

namespace ProductsAPI.Services.Impl;

public class CategoriesService : ICategoriesService
{
    private readonly CatalogApiContext _catalogApiContext;

    public CategoriesService(CatalogApiContext catalogApiContext)
    {
        _catalogApiContext = catalogApiContext;
    }

    public async Task<List<Category>> FindAllCategoriesAsync()
    {
        var categories = await _catalogApiContext.Categories.ToListAsync();
        return categories;
    }

    public async Task<Category> FindCategoryByIdAsync(int id)
    {
        return await CheckIfCategoryExists(id);
    }

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        _catalogApiContext.Categories.Add(category);
        await _catalogApiContext.SaveChangesAsync();
        return category;
    }

    public async Task<Category> UpdateCategoryAsync(int id, Category category)
    {
        await CheckIfCategoryExists(id);
        _catalogApiContext.Entry(category).State = EntityState.Modified;
        await _catalogApiContext.SaveChangesAsync();
        return category;
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await CheckIfCategoryExists(id);
        _catalogApiContext.Categories.Remove(category);
        await _catalogApiContext.SaveChangesAsync();
    }
    
    private async Task<Category> CheckIfCategoryExists(int id)
    {
        Category category = await _catalogApiContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

        if (category is null) {
            throw new CategoryNotFoundException("Category not found with ID: " + id);
        }
        
        return category;
    }
}