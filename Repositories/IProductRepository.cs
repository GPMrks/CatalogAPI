using CatalogAPI.Entities;
using CatalogAPI.Pagination;

namespace CatalogAPI.Repositories;

public interface IProductRepository : IRepository<Product>
{
    IEnumerable<Product> GetProductsSortedByPrice();

    PagedList<Product> GetProducts(ProductsParameters productsParameters);
}
