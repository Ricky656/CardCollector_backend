using Microsoft.AspNetCore.Mvc;
using CardCollector_backend.Dtos.Packs;
using CardCollector_backend.Services.Interfaces;
using CardCollector_backend.Services;

namespace CardCollector_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacksController : ControllerBase
    {
        private readonly IPackService _packService;

        public PacksController(IPackService packService)
        {
            _packService = packService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPackResponseDto>>> GetPacks()
        {
            IEnumerable<GetPackResponseDto> packDtos = await _packService.GetPacks();
            return Ok(packDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPackResponseDto?>> GetPack(long id)
        {
            GetPackResponseDto? packDto = await _packService.GetPack(id);
            return packDto == null ? NotFound() : Ok(packDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetPackResponseDto?>> PutPack(long id, UpdatePackRequestDto packDto)
        {
            if (id != packDto.Id) { return BadRequest(); }
            GetPackResponseDto? pack = await _packService.UpdatePack(id, packDto);
            return pack == null ? BadRequest() : Ok(pack);
        }

        [HttpPost]
        public async Task<ActionResult<GetPackResponseDto?>> PostPack(CreatePackRequestDto packDto)
        {
            GetPackResponseDto? pack = await _packService.AddPack(packDto);
            return pack == null? BadRequest() : Ok(pack);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePack(long id)
        {
            GetPackResponseDto? pack = await _packService.DeletePack(id);
            return pack == null ? NotFound() : NoContent();
        }
    }
}