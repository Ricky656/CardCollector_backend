
namespace CardCollector_backend.Dtos.Users;

public class RefreshLoginDto()
{
    public long UserId { get; set; }
    public string? RefreshToken { get; set; }
}