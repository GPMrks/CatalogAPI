using CatalogAPI.Entities;

namespace CatalogAPI.Repositories;

public interface IProductRepository : IRepository<Product>
{
    IEnumerable<Product> FindProductsSortedByPrice();
}