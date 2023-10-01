using CatalogAPI.Context;
using CatalogAPI.Entities;

namespace CatalogAPI.Repositories.Impl;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(CatalogApiContext catalogApiContext) : base(catalogApiContext)
    {
    }

    public IEnumerable<Product> FindProductsSortedByPrice()
    {
        return FindAll().OrderBy(p => p.Price).ToList();
    }
}