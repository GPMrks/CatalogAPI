using System.Linq.Expressions;
using CatalogAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected CatalogApiContext _catalogApiContext;

    public Repository(CatalogApiContext catalogApiContext)
    {
        _catalogApiContext = catalogApiContext;
    }

    public IQueryable<T> FindAll()
    {
        return _catalogApiContext.Set<T>().AsNoTracking();
    }

    public T FindById(Expression<Func<T, bool>> expression)
    {
        return _catalogApiContext.Set<T>().SingleOrDefault(expression);
    }

    public void Add(T t)
    {
        _catalogApiContext.Set<T>().Add(t);
    }

    public void Update(T t)
    {
        _catalogApiContext.Entry(t).State = EntityState.Modified;
        _catalogApiContext.Set<T>().Update(t);
    }

    public void Delete(T t)
    {
        _catalogApiContext.Set<T>().Remove(t);
    }
}