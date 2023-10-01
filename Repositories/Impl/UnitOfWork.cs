using CatalogAPI.Context;

namespace CatalogAPI.Repositories.Impl;

public class UnitOfWork : IUnitOfWork
{
    private ProductRepository _productRepository;
    private CategoryRepository _categoryRepository;
    private readonly CatalogApiContext _catalogApiContext;

    public UnitOfWork(CatalogApiContext catalogApiContext)
    {
        _catalogApiContext = catalogApiContext;
    }

    public IProductRepository ProductRepository
    {
        get
        {
            return _productRepository ??= new ProductRepository(_catalogApiContext);
        }
    }

    public ICategoryRepository CategoryRepository
    {
        get
        {
            return _categoryRepository ??= new CategoryRepository(_catalogApiContext);
        }
    }

    public void Commit()
    {
        _catalogApiContext.SaveChanges();
    }

    public void Dispose()
    {
        _catalogApiContext.Dispose();
    }
}