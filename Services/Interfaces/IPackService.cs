
using CardCollector_backend.Dtos.Packs;

namespace CardCollector_backend.Services;

public interface IPackService
{
    Task<IEnumerable<GetPackResponseDto>> GetPacks();
    Task<GetPackResponseDto?> GetPack(long id);
    Task<GetPackResponseDto?> AddPack(CreatePackRequestDto packDto);
    Task<GetPackResponseDto?> UpdatePack(long id, UpdatePackRequestDto packDto);
    Task<GetPackResponseDto?> DeletePack(long id);

}