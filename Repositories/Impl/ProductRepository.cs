using CatalogAPI.Context;
using CatalogAPI.Entities;
using CatalogAPI.Pagination;

namespace CatalogAPI.Repositories.Impl;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(CatalogApiContext catalogApiContext) : base(catalogApiContext)
    {
    }

    public IEnumerable<Product> GetProductsSortedByPrice()
    {
        return FindAll().OrderBy(p => p.Price).ToList();
    }

    public PagedList<Product> GetProducts(ProductsParameters productsParameters)
    {
        return PagedList<Product>
                .ToPagedList(FindAll().OrderBy(prod => prod.Id), productsParameters.PageNumber, productsParameters.PageSize);
    }
}