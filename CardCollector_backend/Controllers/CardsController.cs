
using Microsoft.AspNetCore.Mvc;
using CardCollector_backend.Dtos.Cards;
using CardCollector_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CardCollector_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCardResponseDto>>> GetCards()
        {
            IEnumerable<GetCardResponseDto> cardDtos = await _cardService.GetCards();
            return Ok(cardDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCardResponseDto>> GetCard(long id)
        {
            /*var card = await _context.Cards
                .Include("UserCards")
                .FirstOrDefaultAsync(x => x.Id == id);*/
            GetCardResponseDto? cardDto = await _cardService.GetCard(id);
            return cardDto == null ? NotFound() : Ok(cardDto);
        }

        [Authorize(Roles ="Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<GetCardResponseDto?>> PutCard(long id, UpdateCardRequestDto cardDto)
        {
            if (id != cardDto.Id) { return BadRequest(); }
            GetCardResponseDto? card = await _cardService.UpdateCard(id, cardDto);
            return card == null ? NotFound() : Ok(card);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<ActionResult<GetCardResponseDto>> PostCard(CreateCardRequestDto cardDto)
        {
            GetCardResponseDto card = await _cardService.AddCard(cardDto);
            return Ok(card);

        }

        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(long id)
        {
            GetCardResponseDto? card = await _cardService.DeleteCard(id);
            return card == null ? NotFound() : NoContent();
        }
    }
}
