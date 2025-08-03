using System.ComponentModel.DataAnnotations;

namespace CardCollector_backend.Models;

public class Pack
{
    public long Id { get; set; }
    [Required]
    public string Name { get; set; }
    
    public ICollection<Card> Cards { get; set; } = null!;
}