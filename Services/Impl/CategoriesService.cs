using CatalogAPI.Context;
using CatalogAPI.DTOs;
using CatalogAPI.Entities;
using CatalogAPI.Exceptions;
using CatalogAPI.Pagination;
using CatalogAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Services.Impl;

public class CategoriesService : ICategoriesService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoriesService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public List<CategoryDTO> GetAllCategories(CategoriesParameters categoriesParameters)
    {
        var categories = _unitOfWork.CategoryRepository.GetCategories(categoriesParameters);

        return categories.Select(category => new CategoryDTO(category)).ToList();
    }

    public List<CategoryDTO> GetProductsInCategories()
    {
        var categoriesAndProducts = _unitOfWork.CategoryRepository.GetCategoryProducts();
        
        return categoriesAndProducts.Select(category => new CategoryDTO(category)).ToList();
    }

    public CategoryDTO GetCategoryById(int id)
    {
        Category category = CheckIfCategoryExists(id);
        return new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            ImageUrl = category.ImageUrl
        };
    }

    public CategoryDTO CreateCategory(CategoryForm categoryForm)
    {
        Category category = new Category(categoryForm.Name, categoryForm.ImageUrl);
        _unitOfWork.CategoryRepository.Add(category);
        _unitOfWork.Commit();
        return new CategoryDTO(category);
    }

    public CategoryDTO UpdateCategory(int id, CategoryForm categoryForm)
    {
        try
        {
            Category category = CheckIfCategoryExists(id);
            category.Name = categoryForm.Name;
            category.ImageUrl = categoryForm.ImageUrl;
            _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Commit();
            return new CategoryDTO(category);
        }
        catch (DbUpdateException)
        {
            throw new CannotUpdateCategoryException();
        }
    }

    public CategoryDTO UpdateCategoryPatch(int id, CategoryFormPatch categoryFormPatch)
    {
        try
        {
            Category category = CheckIfCategoryExists(id);
            category.Name = categoryFormPatch.Name ?? category.Name;
            category.ImageUrl = categoryFormPatch.ImageUrl ?? category.ImageUrl;
            _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Commit();
            return new CategoryDTO(category);
        }
        catch (DbUpdateException)
        {
            throw new CannotUpdateCategoryException();
        }
    }

    public void DeleteCategory(int id)
    {
        var category = CheckIfCategoryExists(id);
        _unitOfWork.CategoryRepository.Delete(category);
        _unitOfWork.Commit();
    }

    private Category CheckIfCategoryExists(int id)
    {
        var category = _unitOfWork.CategoryRepository.FindById(p => p.Id == id);

        if (category is null) throw new CategoryNotFoundException("Category not found with ID: " + id);

        return category;
    }
}