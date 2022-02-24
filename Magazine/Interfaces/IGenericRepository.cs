namespace Magazine.Interfaces;
public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,
        string? includeProperties = null);
    Task<T> GetAsync(Expression<Func<T, bool>> filter,
        string? includeProperties = null);
    Task Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
}