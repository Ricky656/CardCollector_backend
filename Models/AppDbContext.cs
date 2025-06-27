using Microsoft.EntityFrameworkCore;

namespace CardCollector_backend.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<Card> Cards { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserCard> UserCards { get; set; } = null;
}