using CardCollector_backend.Dtos.Card;
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
    public async Task AddCard(CreateCardRequestDto cardDto)
    {
        Card card = cardDto.ToCardFromCreateDto();
        await _cardRepo.CreateAsync(card);
        await _cardRepo.Save();
        return;
    }

    public async Task DeleteCard(long id)
    {
        Card? card = await _cardRepo.GetByIdAsync(id);
        if (card == null)
        {
            return;
        }
        _cardRepo.Delete(card);
        await _cardRepo.Save();
        return;
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

    public void UpdateCard(long id, UpdateCardRequestDto cardDto)
    {
        Card card = cardDto.ToCardFromUpdateDto();
        _cardRepo.Update(card);
        _cardRepo.Save();
        return;
    }
}