namespace Magazine.Interfaces;
public interface IUnitOfWork
{
    ICategoryRepository CategoryRepository { get; }
    Task<bool> SaveAsync();
}