using CatalogAPI.Entities;

namespace CatalogAPI.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    IEnumerable<Category> FindCategoryProducts();
}