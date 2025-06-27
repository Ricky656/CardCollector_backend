using CardCollector_backend.Enums;
using CardCollector_backend.Models;

namespace CardCollector_backend.Dtos.Card;

public class GetCardResponseDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public CardRarity Rarity { get; set; }

    public ICollection<UserCard> UserCards { get; set; } = null!;
}