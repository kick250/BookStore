using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;
public class BookStoreContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    public BookStoreContext(DbContextOptions<BookStoreContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookStoreContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}