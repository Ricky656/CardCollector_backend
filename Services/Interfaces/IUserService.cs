using CardCollector_backend.Dtos.Users;
using CardCollector_backend.Dtos.UserCards;

namespace CardCollector_backend.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<GetUserResponseDto>> GetUsers();
    Task<GetUserResponseDto?> GetUser(long id);
    Task<GetUserResponseDto?> GetUserCards(long id);
    Task<GetUserResponseDto> AddUser(CreateUserRequestDto userDto);
    Task<GetUserResponseDto?> UpdateUser(long id, UpdateUserRequestDto userDto);
    Task<GetUserResponseDto?> DeleteUser(long id);
    Task<GetUserCardResponseDto?> AddUserCard(CreateUserCardRequestDto userCardDto);
    Task<GetUserCardResponseDto?> DeleteUserCard(long id, long cardId);
}