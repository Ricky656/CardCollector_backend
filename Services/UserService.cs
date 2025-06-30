using CardCollector_backend.Services.Interfaces;
using CardCollector_backend.Repositories.Interfaces;
using CardCollector_backend.Mappers;
using CardCollector_backend.Models;
using CardCollector_backend.Dtos.Users;
using CardCollector_backend.Dtos.UserCards;

namespace CardCollector_backend.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo;
    private readonly IUserCardService _userCardService;

    public UserService(IUserRepository repository, IUserCardService cardService)
    {
        _userRepo = repository;
        _userCardService = cardService;
    }
    public async Task<GetUserResponseDto> AddUser(CreateUserRequestDto userDto)
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

    public async Task<GetUserResponseDto?> GetUserCards(long id)
    {
        User? user = await _userRepo.GetByIdAsync(id);
        if (user == null)
        {
            return null;
        }
        user.UserCards = await _userCardService.GetUserCards(id);
        return user.ToGetUserResponseDto();
    }

    public async Task<IEnumerable<GetUserResponseDto>> GetUsers()
    {
        IEnumerable<User> users = await _userRepo.GetAllAsync();
        IEnumerable<GetUserResponseDto> userDtos = users.Select(s => s.ToGetUserResponseDto());
        return userDtos;
    }

    public async Task<GetUserResponseDto?> UpdateUser(long id, UpdateUserRequestDto userDto)
    {
        if (await _userRepo.UserExists(id) == false)
        {
            return null;
        }
        User? user = userDto.ToUserFromUpdateDto();
        user = await _userRepo.Update(user);
        return user?.ToGetUserResponseDto();
    }

    public async Task<GetUserCardResponseDto?> AddUserCard(CreateUserCardRequestDto userCardDto)
    {
        UserCard userCard = userCardDto.ToUserCardFromCreateDto();
        if (await _userRepo.UserExists(userCard.UserId) == false)
        {
            return null;
        }
        return await _userCardService.AddUserCard(userCard);
    }

    public async Task<GetUserCardResponseDto?> DeleteUserCard(long userId, long cardId)
    {
        return await _userCardService.DeleteUserCard(userId, cardId);
    }
}