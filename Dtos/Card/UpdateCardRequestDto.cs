using CardCollector_backend.Enums;

namespace CardCollector_backend.Dtos.Card;

public class UpdateCardRequestDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public CardRarity Rarity { get; set; }
}