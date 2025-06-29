using CardCollector_backend.Dtos.Cards;
using CardCollector_backend.Mappers;
using CardCollector_backend.Models;
using CardCollector_backend.Repositories.Interfaces;
using CardCollector_backend.Services.Interfaces;

namespace CardCollector_backend.Services;

public class CardService : ICardService
{
    private readonly ICardRepository _cardRepo;
    public CardService(ICardRepository repository)
    {
        _cardRepo = repository; 
    }
    public async Task<GetCardResponseDto> AddCard(CreateCardRequestDto cardDto)
    {
        Card card = cardDto.ToCardFromCreateDto();
        await _cardRepo.CreateAsync(card);
        return card.ToGetDtoFromCard();
    }

    public async Task<GetCardResponseDto?> DeleteCard(long id)
    {
        Card? card = await _cardRepo.GetByIdAsync(id);
        if (card == null)
        {
            return null;
        }
        await _cardRepo.Delete(card);
        return card.ToGetDtoFromCard();
    }

    public async Task<GetCardResponseDto?> GetCard(long id)
    {
        Card? card = await _cardRepo.GetByIdAsync(id);
        return card?.ToGetDtoFromCard();
    }

    public async Task<IEnumerable<GetCardResponseDto>> GetCards()
    {
        IEnumerable<Card> cards = await _cardRepo.GetAllAsync();
        IEnumerable<GetCardResponseDto> cardDtos = cards.Select(s => s.ToGetDtoFromCard());
        return cardDtos;
    }

    public async Task<GetCardResponseDto?> UpdateCard(long id, UpdateCardRequestDto cardDto)
    {
        if (!_cardRepo.CardExists(id))
        {
            return null;
        }

        var card = cardDto.ToCardFromUpdateDto();
        card = await _cardRepo.Update(card);
        return card?.ToGetDtoFromCard();
    }
}