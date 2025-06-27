using CardCollector_backend.Enums;

namespace CardCollector_backend.Dtos.Card;

public class CreateCardRequestDto
{
    public string Name { get; set; }
    public CardRarity Rarity { get; set; }
}