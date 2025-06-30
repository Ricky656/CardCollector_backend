using Microsoft.EntityFrameworkCore;

namespace CardCollector_backend.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<Card> Cards { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserCard> UserCards { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserCard>()
            .HasOne(d => d.User)
            .WithMany(p => p.UserCards)
            .HasForeignKey(d => d.UserId);

        builder.Entity<UserCard>()
            .HasOne(e => e.Card)
            .WithMany(p => p.UserCards)
            .HasForeignKey(d => d.CardId);

        builder.Entity<UserCard>()
            .Navigation(e => e.Card)
            .AutoInclude();
    }
}