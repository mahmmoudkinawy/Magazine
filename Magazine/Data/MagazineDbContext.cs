﻿namespace Magazine.Data;
public class MagazineDbContext : DbContext
{
    public MagazineDbContext(DbContextOptions<MagazineDbContext> options) : base(options)
    { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Post> Posts { get; set; }

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

        modelBuilder.Entity<Post>()
                .HasData(new Post
                {
                    Id = 1,
                    CategoryId = 2,
                    Content = "Bla bla bla this is a content",
                    CreatedDate = new DateTime(2018, 01, 01)
                });
    }
}