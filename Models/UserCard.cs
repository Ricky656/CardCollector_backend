using System.ComponentModel.DataAnnotations;

namespace CardCollector_backend.Models;

public class UserCard
{
    public long Id { get; set; }
    public long UserId {get; set; }
    public long CardId {get; set; }

    [Required]
    public User User { get; set; }
    [Required]
    public Card Card { get; set; }
}