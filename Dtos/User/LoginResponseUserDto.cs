namespace CardCollector_backend.Dtos.Users;

public class LoginResponseUserDto()
{
    public string Username { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Token { get; set; } = String.Empty;
}