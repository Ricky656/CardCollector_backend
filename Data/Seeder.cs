using CardCollector_backend.Enums;
using CardCollector_backend.Models;

namespace CardCollector_backend.Data;

public static class Seeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.Cards.Any()) { return; }
        var cards = new Card[]
        {
            new(){Name="Sssstately Snake", Rarity=CardRarity.Common},
            new(){Name="Big Bat", Rarity=CardRarity.Legendary},
            new(){Name="Pompous Parakeet", Rarity=CardRarity.Rare},
            new(){Name="Cantankerous Crow", Rarity=CardRarity.Uncommon},
            new(){Name="Brusque Bovine", Rarity=CardRarity.Common},
            new(){Name="Gallant Giraffe", Rarity=CardRarity.Common}
        };
        context.Cards.AddRange(cards);
        context.SaveChanges();

        var user = new User { Username = "TestUser" };
        context.Users.Add(user);
        context.SaveChanges();

        var userCards = new UserCard[]
        {
            new(){UserId=1, CardId=1},
            new(){UserId=1, CardId=2},
            new(){UserId=1, CardId=3},
            new(){UserId=1, CardId=4}
        };
        context.UserCards.AddRange(userCards);
        context.SaveChanges();

        var pack = new Pack
        {
            Name = "Alpha Animals",
            Cards = cards
        };
        context.Packs.Add(pack);
        context.SaveChanges();
    }
}