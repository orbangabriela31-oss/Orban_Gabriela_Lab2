using Microsoft.EntityFrameworkCore;
using Orban_Gabriela_Lab2.Models;
namespace Orban_Gabriela_Lab2.Data;

public class Orban_Gabriela_Lab2Context : DbContext
{
    public Orban_Gabriela_Lab2Context(DbContextOptions<Orban_Gabriela_Lab2Context> options)
        : base(options) { }

    public DbSet<Book> Book { get; set; } = default!;
    public DbSet<Publisher> Publisher { get; set; } = default!;
    public DbSet<Author> Author { get; set; } = default!;
    public DbSet<Category> Category { get; set; } = default!;
    public DbSet<BookCategory> BookCategory { get; set; } = default!;
}
