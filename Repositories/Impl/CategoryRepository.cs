using CatalogAPI.Context;
using CatalogAPI.Entities;
using CatalogAPI.Pagination;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Repositories.Impl;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(CatalogApiContext catalogApiContext) : base(catalogApiContext)
    {
    }

    public IEnumerable<Category> GetCategoryProducts()
    {
        return FindAll().Include(c => c.Products);
    }

    public PagedList<Category> GetCategories(CategoriesParameters categoriesParameters)
    {
        return PagedList<Category>
            .ToPagedList(FindAll().OrderBy(cat => cat.Id), categoriesParameters.PageNumber, categoriesParameters.PageSize);
    }
}