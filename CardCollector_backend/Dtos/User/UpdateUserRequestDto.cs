using System.ComponentModel.DataAnnotations;

namespace CardCollector_backend.Dtos.Users;

public class UpdateUserRequestDto
{
    public long Id { get; set; }

    [Required]
    [StringLength(20, MinimumLength=1, ErrorMessage= "Username must be between 1-20 characters")]
    public string Username { get; set; }
}