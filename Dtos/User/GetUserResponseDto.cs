using CardCollector_backend.Dtos.UserCards;
using CardCollector_backend.Models;

namespace CardCollector_backend.Dtos.Users;

public class GetUserResponseDto()
{
    public long Id { get; set; }
    public string Username { get; set; }

    public ICollection<GetUserCardResponseDto> UserCards { get; set; } = null!;
}