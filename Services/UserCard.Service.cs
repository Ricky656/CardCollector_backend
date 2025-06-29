using CardCollector_backend.Repositories.Interfaces;
using CardCollector_backend.Dtos.UserCards;
using CardCollector_backend.Models;
using CardCollector_backend.Services.Interfaces;
using CardCollector_backend.Mappers;

namespace CardCollector_backend.Services;

public class UserCardService : IUserCardService
{
    private readonly IUserCardRepository _userCardRepo;

    public UserCardService(IUserCardRepository repository)
    {
        _userCardRepo = repository;
    }
    public async Task<GetUserCardResponseDto> AddUserCard(long cardId, long userId)
    {
        UserCard userCard = new()
        {
            CardId = cardId,
            UserId = userId
        };
        await _userCardRepo.CreateAsync(userCard);
        return userCard.ToGetUserCardResponseDto();
    }

    public async Task<GetUserCardResponseDto?> DeleteUserCard(long userCardId)
    {
        UserCard? userCard = await _userCardRepo.GetByIdAsync(userCardId);
        if (userCard == null)
        {
            return null;
        }
        await _userCardRepo.Delete(userCard);
        return userCard?.ToGetUserCardResponseDto();
    }

    public async Task<GetUserCardResponseDto?> GetUserCard(long userCardId)
    {
        UserCard? userCard = await _userCardRepo.GetByIdAsync(userCardId);
        return userCard?.ToGetUserCardResponseDto();
    }

    public async Task<IEnumerable<GetUserCardResponseDto>> GetUserCards(long userId)
    {
        IEnumerable<UserCard> userCards = await _userCardRepo.GetAllByIdAsync(userId);
        return userCards.Select(e => e.ToGetUserCardResponseDto());
    }
}