namespace CardCollector_backend.Models;

public class UserCard
{
    public long Id { get; set; }
    public long UserId {get; set; }
    public long CardId {get; set; }

    public User User { get; set; }
    public Card Card { get; set; }
}