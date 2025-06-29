using CardCollector_backend.Enums;

namespace CardCollector_backend.Dtos.Cards;

public class UpdateCardRequestDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public CardRarity Rarity { get; set; }
}