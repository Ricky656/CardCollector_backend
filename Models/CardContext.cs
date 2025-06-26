using Microsoft.EntityFrameworkCore;

namespace CardCollector_backend.Models;

public class CardContext : DbContext
{
    public CardContext(DbContextOptions<CardContext> options)
        : base(options)
    {
    }
    public DbSet<Card> Cards { get; set; } = null!;

}