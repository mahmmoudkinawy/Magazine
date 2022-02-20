namespace Magazine.Interfaces;
public interface IUnitOfWork
{
    ICategoryRepository CategoryRepository { get; }
    IPostRepository PostRepository { get; }
    Task<bool> SaveAsync();
}