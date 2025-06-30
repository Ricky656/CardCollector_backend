using CardCollector_backend.Dtos.Cards;

namespace CardCollector_backend.Dtos.Packs;

public class UpdatePackRequestDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public ICollection<UpdateCardRequestDto> Cards { get; set; } = null!;
}