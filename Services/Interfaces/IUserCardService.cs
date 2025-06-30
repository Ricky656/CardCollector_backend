using CardCollector_backend.Dtos.UserCards;
using CardCollector_backend.Models;

namespace CardCollector_backend.Services.Interfaces;

public interface IUserCardService
{
    Task<ICollection<UserCard>> GetUserCards(long userId);
    Task<UserCard?> GetUserCard(long userCardId);
    Task<GetUserCardResponseDto?> AddUserCard(UserCard userCard);
    Task<GetUserCardResponseDto?> DeleteUserCard(long userId, long userCardId);
}