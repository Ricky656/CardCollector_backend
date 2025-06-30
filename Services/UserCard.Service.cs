using CardCollector_backend.Repositories.Interfaces;
using CardCollector_backend.Dtos.UserCards;
using CardCollector_backend.Models;
using CardCollector_backend.Services.Interfaces;
using CardCollector_backend.Mappers;

namespace CardCollector_backend.Services;

public class UserCardService : IUserCardService
{
    private readonly IUserCardRepository _userCardRepo;
    private readonly ICardRepository _cardRepo;

    public UserCardService(IUserCardRepository repository, ICardRepository cardRepository)
    {
        _userCardRepo = repository;
        _cardRepo = cardRepository;
    }
    public async Task<GetUserCardResponseDto?> AddUserCard(UserCard userCard)
    {
        Card? card = await _cardRepo.GetByIdAsync(userCard.CardId);
        if (card == null)
        {
            return null;
        }
        userCard.Card = card;
        await _userCardRepo.CreateAsync(userCard);
        return userCard.ToGetUserCardResponseDto();
    }

    public async Task<GetUserCardResponseDto?> DeleteUserCard(long userId, long userCardId)
    {
        //TODO: Mismatched Ids should return BadRequest(), difficult to achieve that without changing how
        //services pass back up to controllers, using some encsulating Response object - altenatively throw/catch
        //errors to achieve this behaviour. 
        UserCard? userCard = await _userCardRepo.GetByIdAsync(userCardId);
        if (userCard == null || userCard.UserId != userId)
        {
            return null;
        }
        await _userCardRepo.Delete(userCard);
        return userCard?.ToGetUserCardResponseDto();
    }

    public async Task<UserCard?> GetUserCard(long userCardId)
    {
        UserCard? userCard = await _userCardRepo.GetByIdAsync(userCardId);
        return userCard;
    }

    public async Task<ICollection<UserCard>> GetUserCards(long userId)
    {
        ICollection<UserCard> userCards = (ICollection<UserCard>)await _userCardRepo.GetAllByIdAsync(userId);
        return userCards;
    }
}