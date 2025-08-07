using System.ComponentModel.DataAnnotations;
using CardCollector_backend.Enums;
using NuGet.Protocol.Plugins;

namespace CardCollector_backend.Models;

public class Card
{
    public long Id { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "Card name must be between 1-20 characters")]
    public string Name { get; set; }
    public CardRarity Rarity { get; set; }

    public ICollection<UserCard> UserCards { get; set; } = null!;
    public ICollection<Pack> Packs { get; } = null!;
}