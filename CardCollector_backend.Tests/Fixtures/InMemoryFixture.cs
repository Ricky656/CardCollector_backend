using CardCollector_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CardCollector_backend.Tests.Fixtures;

public class InMemoryFixture : IDisposable
{
    public AppDbContext _context { get; private set; }
    public InMemoryFixture()
    {
        Reset();
    }

    public void Reset()
    {
        CreateDatabase();
        SeedData();
    }
    public void Dispose()
    {
        _context.Dispose();
    }

    public void CreateDatabase()
    {
        var contextOptions = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        _context = new AppDbContext(contextOptions);
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    public void SeedData()
    {

        Card testCard = new()
        {
            Name = "TestCard",
            Rarity = Enums.CardRarity.Common
        };
        _context.Add(testCard);

        User testUser = new() { Username = "TestUser", Email = "user@test.com", Role = "User" };
        testUser.PasswordHash = new PasswordHasher<User>().HashPassword(testUser, "password");

        _context.Add(testUser);
        _context.SaveChanges();

        _context.Add(new UserCard()
        {
            UserId = 1,
            CardId = 1
        });

        Pack testPack = new()
        {
            Name = "TestPack",
            Cards = [testCard]
        };

        _context.Add(testPack);
        _context.SaveChanges();

        _context.ChangeTracker.Clear();
    }
}