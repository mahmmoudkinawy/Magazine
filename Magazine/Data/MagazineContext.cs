namespace Magazine.Data;
public class MagazineContext : DbContext
{
    public MagazineContext(DbContextOptions<MagazineContext> options) : base(options)
    { }

    public DbSet<Category> Categories { get; set; }

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
    }
}