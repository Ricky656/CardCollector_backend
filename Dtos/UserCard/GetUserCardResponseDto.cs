namespace CardCollector_backend.Dtos.UserCards;

public class GetUserCardResponseDto
{
    public long Id { get; set; }
    public long UserId {get; set; }
    public long CardId {get; set; }
}