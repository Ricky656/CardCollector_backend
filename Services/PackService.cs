using CardCollector_backend.Dtos.Packs;
using CardCollector_backend.Mappers;
using CardCollector_backend.Models;
using CardCollector_backend.Repositories.Interfaces;

namespace CardCollector_backend.Services;
public class PackService : IPackService
{
    private readonly IPackRepository _packRepo;

    public PackService(IPackRepository repository)
    {
        _packRepo = repository;
    }
    public async Task<GetPackResponseDto> AddPack(CreatePackRequestDto packDto)
    {
        Pack pack = packDto.ToPackFromCreateDto();
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
        Pack pack = packDto.ToPackFromUpdateDto();
        await _packRepo.Update(pack);
        return pack.ToGetDtoFromPack();
    }
}