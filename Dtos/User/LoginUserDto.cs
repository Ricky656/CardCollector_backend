using System.Reflection.Metadata;

namespace CardCollector_backend.Dtos.Users;

public class LoginUserDto()
{
    public string Email { get; set; } = String.Empty;
    public string Username { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
}