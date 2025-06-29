using CardCollector_backend.Models;
using CardCollector_backend.Dtos.UserCards;

namespace CardCollector_backend.Mappers;

public static class UserCardMappers
{
    public static UserCard ToUserCardFromCreateDto(this CreateUserCardRequestDto userCard)
    {
        return new UserCard
        {
            UserId = userCard.UserId,
            CardId = userCard.CardId
        };
    }

    public static GetUserCardResponseDto ToGetUserCardResponseDto(this UserCard userCard)
    {
        return new GetUserCardResponseDto
        {
            Id = userCard.Id,
            UserId = userCard.UserId,
            CardId = userCard.CardId
        };
    }
}