using System.ComponentModel.DataAnnotations;
using CardCollector_backend.Dtos.Cards;

namespace CardCollector_backend.Dtos.Packs;

public class CreatePackRequestDto
{
    [Required]
    public string Name { get; set; }

    public ICollection<int> CardIds { get; set; } = null!;
}