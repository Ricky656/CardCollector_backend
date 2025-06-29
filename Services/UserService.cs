using CardCollector_backend.Services.Interfaces;
using CardCollector_backend.Repositories.Interfaces;
using CardCollector_backend.Mappers;
using CardCollector_backend.Models;
using CardCollector_backend.Dtos.Users;

namespace CardCollector_backend.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo;

    public UserService(IUserRepository repository)
    {
        _userRepo = repository;
    }
    public async Task<GetUserResponseDto?> AddUser(CreateUserRequestDto userDto)
    {
        User user = userDto.ToUserFromCreateDto();
        await _userRepo.CreateAsync(user);
        return user.ToGetUserResponseDto();
    }

    public async Task<GetUserResponseDto?> DeleteUser(long id)
    {
        User? user = await _userRepo.GetByIdAsync(id);
        if (user == null)
        {
            return null;
        }
        await _userRepo.Delete(user);
        return user.ToGetUserResponseDto();
    }

    public async Task<GetUserResponseDto?> GetUser(long id)
    {
        User? user = await _userRepo.GetByIdAsync(id);
        return user?.ToGetUserResponseDto();
    }

    public async Task<IEnumerable<GetUserResponseDto>> GetUsers()
    {
        IEnumerable<User> users = await _userRepo.GetAllAsync();
        IEnumerable<GetUserResponseDto> userDtos = users.Select(s => s.ToGetUserResponseDto());
        return userDtos;
    }

    public Task<GetUserResponseDto?> UpdateUser(long id, UpdateUserRequestDto user)
    {
        throw new NotImplementedException();
    }
}