using System.ComponentModel.DataAnnotations;
using CardCollector_backend.Dtos.Cards;

namespace CardCollector_backend.Dtos.UserCards;

public class GetUserCardResponseDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long CardId { get; set; }
    public GetCardResponseDto Card { get; set; }
}