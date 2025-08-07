using CardCollector_backend.Dtos.Cards;
using CardCollector_backend.Models;

namespace CardCollector_backend.Mappers;

public static class CardMappers
{
    public static Card ToCardFromCreateDto(this CreateCardRequestDto card)
    {
        return new Card
        {
            Name = card.Name,
            Rarity = card.Rarity
        };
    }

    public static GetCardResponseDto ToGetDtoFromCard(this Card card)
    {
        return new GetCardResponseDto
        {
            Id = card.Id,
            Name = card.Name,
            Rarity = card.Rarity,
            //UserCards = card.UserCards
        };
    }

    public static Card ToCardFromUpdateDto(this UpdateCardRequestDto card)
    {
        return new Card
        {
            Id = card.Id,
            Name = card.Name,
            Rarity = card.Rarity
        };
    }
}