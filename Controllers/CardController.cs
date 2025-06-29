
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CardCollector_backend.Models;
using CardCollector_backend.Dtos.Cards;
using CardCollector_backend.Mappers;
using CardCollector_backend.Services;
using CardCollector_backend.Services.Interfaces;

namespace CardCollector_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ICardService _cardService;

        public CardController(AppDbContext context, ICardService cardService)
        {
            _cardService = cardService;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCardResponseDto>>> GetCards()
        {
            /*var cards = await _context.Cards.Include("UserCards").ToListAsync();
            var cardDtos = cards.Select(s => s.ToGetDtoFromCard());*/
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

        [HttpPut("{id}")]
        public async Task<ActionResult<GetCardResponseDto?>> PutCard(long id, UpdateCardRequestDto cardDto)
        {
            if (id != cardDto.Id) { return BadRequest(); }
            var card = await _cardService.UpdateCard(id, cardDto);
            if (card == null)
            {
                return NotFound();
            }
            return card;
        }

        [HttpPost]
        public async Task<ActionResult<GetCardResponseDto>> PostCard(CreateCardRequestDto cardDto)
        {
            var card = await _cardService.AddCard(cardDto);
            return card;

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(long id)
        {
            var card = await _cardService.DeleteCard(id);
            if (card == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
