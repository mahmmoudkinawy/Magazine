namespace Magazine.Repositories;
public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(MagazineDbContext context) : base(context)
    {
    }
}