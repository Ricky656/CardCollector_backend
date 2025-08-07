
using CardCollector_backend.Dtos.Cards;

namespace CardCollector_backend.Dtos.Packs;

public class GetPackResponseDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public ICollection<GetCardResponseDto> Cards { get; set; } = null!;
}