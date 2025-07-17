using CardCollector_backend.Dtos.Packs;
using CardCollector_backend.Mappers;
using CardCollector_backend.Models;
using CardCollector_backend.Repositories.Interfaces;
using CardCollector_backend.Services.Interfaces;

namespace CardCollector_backend.Services;

public class PackService : IPackService
{
    private readonly IPackRepository _packRepo;
    private readonly ICardRepository _cardRepo;

    public PackService(IPackRepository packRepository, ICardRepository cardRepository)
    {
        _packRepo = packRepository;
        _cardRepo = cardRepository;
    }
    public async Task<GetPackResponseDto?> AddPack(CreatePackRequestDto packDto)
    {
        Pack pack = packDto.ToPackFromCreateDto();
        ICollection<Card>? cards = await GetCardsFromIds(packDto.CardIds);
        if (cards == null) { return null; }
        pack.Cards = cards;
        await _packRepo.CreateAsync(pack);
        return pack.ToGetDtoFromPack();
    }

    public async Task<GetPackResponseDto?> DeletePack(long id)
    {
        Pack? pack = await _packRepo.GetByIdAsync(id);
        if (pack == null)
        {
            return null;
        }
        await _packRepo.Delete(id);
        return pack.ToGetDtoFromPack();
    }

    public async Task<GetPackResponseDto?> GetPack(long id)
    {
        Pack? pack = await _packRepo.GetByIdAsync(id);
        return pack == null ? null : pack.ToGetDtoFromPack();
    }

    public async Task<IEnumerable<GetPackResponseDto>> GetPacks()
    {
        IEnumerable<Pack> packs = await _packRepo.GetAllAsync();
        IEnumerable<GetPackResponseDto> packDtos = packs.Select(e => e.ToGetDtoFromPack());
        return packDtos;
    }

    public async Task<GetPackResponseDto?> UpdatePack(long id, UpdatePackRequestDto packDto)
    {
        if (!await _packRepo.PackExists(id))
        {
            return null;
        }
        Pack? pack = packDto.ToPackFromUpdateDto();
        ICollection<Card>? cards = await GetCardsFromIds(packDto.CardIds);
        if (cards == null) { return null; }
        pack.Cards = cards;
        pack = await _packRepo.Update(pack);
        return pack?.ToGetDtoFromPack();
    }

    private async Task<ICollection<Card>?> GetCardsFromIds(IEnumerable<int> cardIds)
    {
        ICollection<Card> cards = [];
        foreach (int cardId in cardIds)
        {
            Card? card = await _cardRepo.GetByIdAsync(cardId);
            if (card == null) { return null; }
            cards.Add(card);
        }
        return cards;
    }

    /*private ICollection<Card>? UpdateCardList(Pack pack, IEnumerable<int> cardIds)
    {
        ICollection<long> _cardIds = (ICollection<long>)cardIds;
        foreach (Card card in pack.Cards)
        {
            if (!_cardIds.Contains(card.Id))
            {
                pack.Cards.Remove(card);
            }
        }
    }*/
}