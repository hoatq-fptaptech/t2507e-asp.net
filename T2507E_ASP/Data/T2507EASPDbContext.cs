using Microsoft.EntityFrameworkCore;
using T2507E_ASP.Entities;

namespace T2507E_ASP.Data;

public class T2507EASPDbContext: DbContext
{
    public T2507EASPDbContext(DbContextOptions options) : base(options)
    {
    }

    // public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Student> Students => Set<Student>();
    public DbSet<User> Users => Set<User>();
    
}