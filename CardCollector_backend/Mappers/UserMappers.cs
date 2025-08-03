using CardCollector_backend.Dtos.Users;
using CardCollector_backend.Dtos.UserCards;
using CardCollector_backend.Models;
using CardCollector_backend.Services;
using Microsoft.AspNetCore.Identity;
namespace CardCollector_backend.Mappers;

public static class UserMappers
{
    public static User ToUserFromCreateDto(this CreateUserRequestDto userDto)
    {
        User user = new()
        {
            Username = userDto.Username,
            Email = userDto.Email
        };
        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, userDto.Password);
        return user;
    }
    public static GetUserResponseDto ToGetUserResponseDto(this User user)
    {
        ICollection<GetUserCardResponseDto> cards = null;
        if (user.UserCards != null)
        {
            cards = [.. user.UserCards.Select(c => c.ToGetUserCardResponseDto())];
        }
        return new GetUserResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            UserCards = cards
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

    /*public static LoginResponseUserDto ToLoginDtoFromUser(this User user)
    {
        return new LoginResponseUserDto
        {
            Username = user.Username,
            Email = user.Email,
            Token = new TokenService().CreateToken(user)
        }
    }*/
}