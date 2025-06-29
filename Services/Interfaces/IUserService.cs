using CardCollector_backend.Dtos.Users;

namespace CardCollector_backend.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<GetUserResponseDto>> GetUsers();
    Task<GetUserResponseDto?> GetUser(long id);
    Task<GetUserResponseDto?> AddUser(CreateUserRequestDto userDto);
    Task<GetUserResponseDto?> UpdateUser(long id, UpdateUserRequestDto userDto);
    Task<GetUserResponseDto?> DeleteUser(long id);
}