namespace Magazine.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly MagazineDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(MagazineDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,
        string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
            query = query.Where(filter);

        if (includeProperties != null)
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

        return await query.ToListAsync();
    }


    public async Task<T> GetAsync(Expression<Func<T, bool>> filter,
        string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;

        query = query.Where(filter);

        if (includeProperties != null)
            foreach (var includePoperty in includeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includePoperty);

        return await query.FirstOrDefaultAsync();
    }

    public async Task Add(T entity)
    {
        _dbSet.Add(entity); //Just try to search about use Add or AddAsync
        await _context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}