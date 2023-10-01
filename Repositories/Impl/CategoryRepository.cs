using CatalogAPI.Context;
using CatalogAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Repositories.Impl;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(CatalogApiContext catalogApiContext) : base(catalogApiContext)
    {
    }

    public IEnumerable<Category> FindCategoryProducts()
    {
        return FindAll().Include(c => c.Products);
    }
}