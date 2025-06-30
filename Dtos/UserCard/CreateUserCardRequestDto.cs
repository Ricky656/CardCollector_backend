using System.ComponentModel.DataAnnotations;

namespace CardCollector_backend.Dtos.UserCards;

public class CreateUserCardRequestDto
{
    //public int Quantity { get; set; }
    [Required]
    public long UserId { get; set; }

    [Required]
    public long CardId { get; set; }
}