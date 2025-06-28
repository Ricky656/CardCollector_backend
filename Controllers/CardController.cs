
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CardCollector_backend.Models;
using CardCollector_backend.Dtos.Card;
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
            if (cardDto == null) { return NotFound(); }

            return cardDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(long id, UpdateCardRequestDto cardDto)
        {
            /*var card = cardDto.ToCardFromUpdateDto();
            if (id != card.Id) { return BadRequest(); }

            _context.Entry(card).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }*/
            _cardService.UpdateCard(id, cardDto);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Card>> PostCard(CreateCardRequestDto cardDto)
        {
            //var card = await _cardService.AddCard(cardDto);
            await _cardService.AddCard(cardDto);
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(long id)
        {
            await _cardService.DeleteCard(id);
            return NoContent();
        }
    }
}
