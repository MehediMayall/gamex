using System.Linq.Expressions;

namespace gamex.Common;

public interface IMongoRepository<T> where T : EntityBase
{
    Task<IReadOnlyCollection<T>> GetAll();
    Task<IReadOnlyCollection<T>> GetAll(Expression<Func<T,bool>> filter);
    Task<T> Get(Guid id);
    Task<T> Get(Expression<Func<T, bool>> filter);
    Task Create(T item);
    Task Update(T item);
    Task Delete(Guid id);
}