using CardCollector_backend.Dtos.UserCards;
using CardCollector_backend.Models;

namespace CardCollector_backend.Services.Interfaces;

public interface IUserCardService
{
    Task<IEnumerable<GetUserCardResponseDto>> GetUserCards(long userId);
    Task<GetUserCardResponseDto?> GetUserCard(long userCardId);
    Task<GetUserCardResponseDto> AddUserCard(long cardId, long userId);
    Task<GetUserCardResponseDto?> DeleteUserCard(long userCardId);
}