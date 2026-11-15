using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Orban_Gabriela_Lab2.Data;

public class LibraryIdentityContext : IdentityDbContext<IdentityUser>
{
    public LibraryIdentityContext(DbContextOptions<LibraryIdentityContext> options)
        : base(options) { }
}
