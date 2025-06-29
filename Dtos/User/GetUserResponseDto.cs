using CardCollector_backend.Models;

namespace CardCollector_backend.Dtos.Users;

public class GetUserResponseDto()
{
    public long Id { get; set; }
    public string Username { get; set; }

    public ICollection<UserCard> UserCards { get; set; } = null!;
}