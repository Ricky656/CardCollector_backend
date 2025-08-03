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
    public DbSet<Pack> Packs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserCard>()
            .HasOne(e => e.User)
            .WithMany(e => e.UserCards)
            .HasForeignKey(e => e.UserId);

        builder.Entity<UserCard>()
            .HasOne(e => e.Card)
            .WithMany(e => e.UserCards)
            .HasForeignKey(e => e.CardId);

        builder.Entity<UserCard>()
            .Navigation(e => e.Card)
            .AutoInclude();

        builder.Entity<Pack>()
            .HasMany(e => e.Cards)
            .WithMany(e => e.Packs);
    }
}