using CardCollector_backend.Dtos.Users;
using CardCollector_backend.Models;

namespace CardCollector_backend.Mappers;

public static class UserMappers
{
    public static User ToUserFromCreateDto(this CreateUserRequestDto user)
    {
        return new User
        {
            Username = user.Username
        };
    }
    public static GetUserResponseDto ToGetUserResponseDto(this User user)
    {
        return new GetUserResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            UserCards = user.UserCards
        };
    }

    public static User ToUserFromUpdateDto(this UpdateUserRequestDto user)
    {
        return new User
        {
            Id = user.Id,
            Username = user.Username
        };
    }
}