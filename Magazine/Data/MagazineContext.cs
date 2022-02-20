namespace Magazine.Data;
public class MagazineContext : DbContext
{
    public MagazineContext(DbContextOptions<MagazineContext> options) : base(options)
    { }

    public DbSet<Category> Categories { get; set; }
}