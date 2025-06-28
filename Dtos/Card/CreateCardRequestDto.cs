using System.ComponentModel.DataAnnotations;
using CardCollector_backend.Enums;

namespace CardCollector_backend.Dtos.Card;

public class CreateCardRequestDto
{
    [Required]
    [StringLength(20, MinimumLength=1, ErrorMessage="Card name must be between 1-20 characters")]
    public string Name { get; set; }
    public CardRarity Rarity { get; set; }
}