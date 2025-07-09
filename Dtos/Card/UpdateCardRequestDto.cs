using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CardCollector_backend.Enums;

namespace CardCollector_backend.Dtos.Cards;

public class UpdateCardRequestDto
{
    public long Id { get; set; }
    
    [StringLength(20, MinimumLength = 1, ErrorMessage = "Card name must be between 1-20 characters")]
    public string Name { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CardRarity Rarity { get; set; }
}