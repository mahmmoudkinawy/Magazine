namespace Magazine.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly MagazineContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(MagazineContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;

        if (includeProperties != null)
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

        return await query.ToListAsync();
    }


    public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = _dbSet;

        query = query.Where(filter);

        return await query.FirstOrDefaultAsync();
    }

    public void Add(T entity) => _dbSet.Add(entity);

    public void Update(T entity) => _dbSet.Update(entity);

    public void Delete(T entity) => _dbSet.Remove(entity);

}