namespace Magazine.Repositories;
public class PostRepository : GenericRepository<Post>, IPostRepository
{
    public PostRepository(MagazineContext context) : base(context)
    {
    }
}