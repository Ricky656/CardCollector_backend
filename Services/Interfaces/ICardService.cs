using CardCollector_backend.Dtos.Card;
using CardCollector_backend.Models;
using CardCollector_backend.Repositories.Interfaces;

namespace CardCollector_backend.Services.Interfaces;

public interface ICardService
{
    Task<IEnumerable<GetCardResponseDto>> GetCards();
    Task<GetCardResponseDto?> GetCard(long id);
    Task<GetCardResponseDto> AddCard(CreateCardRequestDto card);
    Task<GetCardResponseDto?> UpdateCard(long id, UpdateCardRequestDto card);
    Task<GetCardResponseDto?> DeleteCard(long id);
}