namespace Magazine.Repositories;
public class PostRepository : GenericRepository<Post>, IPostRepository
{
    public PostRepository(MagazineDbContext context) : base(context)
    {
    }
}