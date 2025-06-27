using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CardCollector_backend.Models;
using CardCollector_backend.Dtos.Card;
using CardCollector_backend.Mappers;

namespace CardCollector_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCardResponseDto>>> GetCards()
        {
            var cards = await _context.Cards.Include("UserCards").ToListAsync();
            var cardDtos = cards.Select(s => s.ToGetDtoFromCard());

            return Ok(cardDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCardResponseDto>> GetCard(long id)
        {
            var card = await _context.Cards
                .Include("UserCards")
                .FirstOrDefaultAsync(x => x.Id == id);

            if (card == null) { return NotFound(); }

            return card.ToGetDtoFromCard();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(long id, UpdateCardRequestDto cardDto)
        {
            var card = cardDto.ToCardFromUpdateDto();
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
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Card>> PostCard(CreateCardRequestDto cardDto)
        {
            var card = cardDto.ToCardFromCreateDto();
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCard), new { id = card.Id }, card);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(long id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null) { return NotFound(); }

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardExists(long id)
        {
            return _context.Cards.Any(e => e.Id == id);
        }
    }
}
