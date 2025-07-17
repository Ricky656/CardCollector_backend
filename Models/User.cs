using System.ComponentModel.DataAnnotations;

namespace CardCollector_backend.Models;

public class User
{
    public long Id { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "Username must be between 1-20 characters")]
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ICollection<UserCard> UserCards { get; set; } = null!;
}