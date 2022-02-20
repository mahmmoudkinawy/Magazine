namespace Magazine.Repositories;
public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(MagazineContext context) : base(context)
    {
    }
}