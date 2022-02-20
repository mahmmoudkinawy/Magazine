namespace Magazine.Interfaces;
public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,
        string? includeProperties = null);
    Task<T> GetAsync(Expression<Func<T, bool>> filter);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}