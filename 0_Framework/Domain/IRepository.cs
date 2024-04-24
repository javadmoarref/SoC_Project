using System.Linq.Expressions;

namespace _0_Framework.Domain;

public interface IRepository<TKey,T> where T:class
{
    void Create(T entity);
    T Get(TKey id);
    List<T> Get();
    void SaveChanges();
    bool Exists(Expression<Func<T,bool>> expression);
}