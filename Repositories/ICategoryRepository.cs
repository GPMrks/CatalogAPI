using CatalogAPI.Entities;
using CatalogAPI.Pagination;

namespace CatalogAPI.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    IEnumerable<Category> GetCategoryProducts();

    PagedList<Category> GetCategories(CategoriesParameters categoriesParameters);
}