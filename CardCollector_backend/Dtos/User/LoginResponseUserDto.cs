namespace CardCollector_backend.Dtos.Users;

public class LoginResponseUserDto()
{
    public long Id { get; set; } 
    public string Username { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Role { get; set; } = String.Empty;
    public string Token { get; set; } = String.Empty;
    public string RefreshToken { get; set; } = String.Empty;
}