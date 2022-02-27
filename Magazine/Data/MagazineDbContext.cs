namespace Magazine.Data;
public class MagazineDbContext : IdentityDbContext
{
    public MagazineDbContext(DbContextOptions<MagazineDbContext> options) : base(options)
    { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Article> Articles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>()
                .HasData(new Category
                {
                    Id = 1,
                    Name = "Tourism"
                },
                new Category
                {
                    Id = 2,
                    Name = "Programming"
                });

        modelBuilder.Entity<Article>()
                .HasData(new Article
                {
                    Id = 1, 
                    Title = "New Asp.net",
                    CategoryId = 2,
                    Content = "Bla bla bla this is a content",
                    CreatedDate = new DateTime(2018, 01, 01)
                });
    }
}