using CatalogAPI.Context;
using CatalogAPI.Entities;
using CatalogAPI.Entities.DTOs;
using CatalogAPI.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Services.Impl;

public class CategoriesService : ICategoriesService
{
    private readonly CatalogApiContext _catalogApiContext;

    public CategoriesService(CatalogApiContext catalogApiContext)
    {
        _catalogApiContext = catalogApiContext;
    }

    public async Task<List<CategoryDTO>> FindAllCategoriesAsync()
    {
        var categories = await _catalogApiContext.Categories.ToListAsync();
        return categories.Select(category => new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            ImageUrl = category.ImageUrl
        }).ToList();
    }

    public async Task<List<CategoryDTO>> FindProductsInCategories()
    {
        var categoriesAndProducts = await _catalogApiContext.Categories.Include(p => p.Products).ToListAsync();
        return categoriesAndProducts.Select(category => new CategoryDTO(category)).ToList();
    }

    public async Task<CategoryDTO> FindCategoryByIdAsync(int id)
    {
        Category category = await CheckIfCategoryExists(id);
        return new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            ImageUrl = category.ImageUrl
        };
    }

    public async Task<CategoryDTO> CreateCategoryAsync(CategoryForm categoryForm)
    {
        Category category = new Category(categoryForm.Name, categoryForm.ImageUrl);
        _catalogApiContext.Categories.Add(category);
        await _catalogApiContext.SaveChangesAsync();
        return new CategoryDTO(category);
    }

    public async Task<CategoryDTO> UpdateCategoryAsync(int id, CategoryForm categoryForm)
    {
        try
        {
            Category category = await CheckIfCategoryExists(id);
            category.Name = categoryForm.Name ?? category.Name;
            category.ImageUrl = categoryForm.ImageUrl ?? category.ImageUrl;
            _catalogApiContext.Entry(category).State = EntityState.Modified;
            await _catalogApiContext.SaveChangesAsync();
            return new CategoryDTO(category);
        }
        catch (DbUpdateException)
        {
            throw new CannotUpdateCategoryException();
        }
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await CheckIfCategoryExists(id);
        _catalogApiContext.Categories.Remove(category);
        await _catalogApiContext.SaveChangesAsync();
    }

    private async Task<Category> CheckIfCategoryExists(int id)
    {
        var category = await _catalogApiContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

        if (category is null) throw new CategoryNotFoundException("Category not found with ID: " + id);

        return category;
    }
}