using CardCollector_backend.Models;

namespace CardCollector_backend.Data;

public static class Seeder
{
    public static void Seed(AppDbContext context)
    {
        if(context.Cards.Any()){ return; }
        var cards = new Card[]
        {
            new Card{Name="Stately Snake", Rarity=Card.CardRarity.Common},
            new Card{Name="Big Bat", Rarity=Card.CardRarity.Legendary},
            new Card{Name="Pompous Parakeet", Rarity=Card.CardRarity.Rare},
            new Card{Name="Cantankerous Crow", Rarity=Card.CardRarity.Uncommon},
            new Card{Name="Brusque Bovine", Rarity=Card.CardRarity.Common},
            new Card{Name="Gallant Giraffe", Rarity=Card.CardRarity.Common}
        };
        context.Cards.AddRange(cards);
        context.SaveChanges();

        var user = new User{Username="TestUser"};
        context.Users.Add(user);
        context.SaveChanges();

        var userCards = new UserCard[]
        {
            new UserCard{UserId=1, CardId=1},
            new UserCard{UserId=1, CardId=2},
            new UserCard{UserId=1, CardId=3},
            new UserCard{UserId=1, CardId=4}
        };
        context.UserCards.AddRange(userCards);
        context.SaveChanges();
    }
}