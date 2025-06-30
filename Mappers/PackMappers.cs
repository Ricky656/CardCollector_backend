using CardCollector_backend.Dtos.Cards;
using CardCollector_backend.Dtos.Packs;
using CardCollector_backend.Models;

namespace CardCollector_backend.Mappers;

public static class PackMappers
{
    public static GetPackResponseDto ToGetDtoFromPack(this Pack pack)
    {
        return new GetPackResponseDto
        {
            Id = pack.Id,
            Name = pack.Name,
            Cards = pack.Cards.ToCardDtoCollection()
        };
    }

    public static Pack ToPackFromCreateDto(this CreatePackRequestDto packDto)
    {
        return new Pack
        {
            Name = packDto.Name,
        };
    }

    public static Pack ToPackFromUpdateDto(this UpdatePackRequestDto packDto)
    {
        return new Pack
        {
            Id = packDto.Id,
            Name = packDto.Name,
        };
    }

    public static ICollection<GetCardResponseDto>? ToCardDtoCollection(this ICollection<Card> cards)
    {
        return cards == null ? null : [.. cards.Select(c => c.ToGetDtoFromCard())];
    }

    /*public static ICollection<Card>? ToCardCollection(this ICollection<UpdateCardRequestDto> cards)
    {
        return cards == null? null : [.. cards.Select(c => c.ToCardFromUpdateDto())];
    }*/
}