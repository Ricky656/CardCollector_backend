using System.ComponentModel.DataAnnotations;

namespace CardCollector_backend.Models;

public class UserCard
{
    public long Id { get; set; }
    [Required]
    public long UserId { get; set; }
    [Required]
    public long CardId { get; set; }
    public User User { get; set; }
    public Card Card { get; set; }
}