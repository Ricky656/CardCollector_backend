using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardCollector_backend.Models;

public class Card
{
    public enum CardRarity
    {
        Common, Uncommon, Rare, Legendary
    }
    public long Id { get; set; }

    [Required]
    [StringLength(20, MinimumLength=1, ErrorMessage="Card name must be between 1-20 characters")]
    public string Name { get; set; }
    public CardRarity Rarity { get; set; }
}