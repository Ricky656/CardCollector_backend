using CardCollector_backend.Enums;
using CardCollector_backend.Models;
using Microsoft.AspNetCore.Identity;

namespace CardCollector_backend.Data;

public static class Seeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.Cards.Any()) { return; }
        Card[] cards =
        [
            new(){Name="Sssstately Snake", Rarity=CardRarity.Common},
            new(){Name="Big Bat", Rarity=CardRarity.Legendary},
            new(){Name="Pompous Parakeet", Rarity=CardRarity.Rare},
            new(){Name="Cantankerous Crow", Rarity=CardRarity.Uncommon},
            new(){Name="Brusque Bovine", Rarity=CardRarity.Common},
            new(){Name="Gallant Giraffe", Rarity=CardRarity.Common}
        ];
        context.Cards.AddRange(cards);
        context.SaveChanges();

        User user = new() { Username = "TestUser", Email = "user@test.com", Role = "User" };
        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, "password");
        context.Users.Add(user);
        user = new User { Username = "TestAdmin", Email = "admin@test.com", Role = "Admin" };
        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, "password");
        context.Users.Add(user);
        context.SaveChanges();

        UserCard[] userCards =
        [
            new(){UserId=1, CardId=1},
            new(){UserId=1, CardId=2},
            new(){UserId=1, CardId=3},
            new(){UserId=1, CardId=4}
        ];
        context.UserCards.AddRange(userCards);
        context.SaveChanges();

        Pack pack = new()
        {
            Name = "Alpha Animals",
            Cards = cards
        };
        context.Packs.Add(pack);
        context.SaveChanges();
    }
}