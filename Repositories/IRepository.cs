using System.Linq.Expressions;

namespace CatalogAPI.Repositories;

public interface IRepository<T>
{
    IQueryable<T> FindAll();
    T FindById(Expression<Func<T, bool>> expression);
    void Add(T t);
    void Update(T t);
    void Delete(T t);
}