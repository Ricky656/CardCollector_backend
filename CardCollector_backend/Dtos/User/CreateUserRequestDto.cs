using System.ComponentModel.DataAnnotations;

namespace CardCollector_backend.Dtos.Users;

public class CreateUserRequestDto()
{
    [Required]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "Username must be between 1-20 characters")]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
    
    [Required]
    public string Email { get; set; }

}