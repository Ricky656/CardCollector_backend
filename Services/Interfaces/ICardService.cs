using CardCollector_backend.Dtos.Cards;

namespace CardCollector_backend.Services.Interfaces;

public interface ICardService
{
    Task<IEnumerable<GetCardResponseDto>> GetCards();
    Task<GetCardResponseDto?> GetCard(long id);
    Task<GetCardResponseDto> AddCard(CreateCardRequestDto cardDto);
    Task<GetCardResponseDto?> UpdateCard(long id, UpdateCardRequestDto cardDto);
    Task<GetCardResponseDto?> DeleteCard(long id);
}