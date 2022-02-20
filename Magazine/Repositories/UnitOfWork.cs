namespace Magazine.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly MagazineContext _context;

    public UnitOfWork(MagazineContext context)
    {
        _context = context;
        CategoryRepository = new CategoryRepository(_context);
        PostRepository = new PostRepository(_context);
    }

    public ICategoryRepository CategoryRepository { get; private set; }
    public IPostRepository PostRepository { get; private set; }

    public async Task<bool> SaveAsync() => await _context.SaveChangesAsync() > 0;
}
