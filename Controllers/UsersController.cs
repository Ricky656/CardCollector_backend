using Microsoft.AspNetCore.Mvc;
using CardCollector_backend.Services.Interfaces;
using CardCollector_backend.Dtos.Users;
using CardCollector_backend.Dtos.UserCards;

namespace CardCollector_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService service)
        {
            _userService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserResponseDto>>> GetUsers()
        {
            IEnumerable<GetUserResponseDto> userDtos = await _userService.GetUsers();
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserResponseDto>> GetUser(long id)
        {
            GetUserResponseDto? userDto = await _userService.GetUser(id);
            return userDto == null ? NotFound() : Ok(userDto);
        }

        [HttpGet("{id}/Cards")]
        public async Task<ActionResult<GetUserResponseDto>> GetUserCards(long id)
        {
            GetUserResponseDto? userDto = await _userService.GetUserCards(id);
            return userDto == null ? NotFound() : Ok(userDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetUserResponseDto>> PutUser(long id, UpdateUserRequestDto userDto)
        {
            if (userDto.Id != id) { return BadRequest(); }
            GetUserResponseDto? user = await _userService.UpdateUser(id, userDto);
            return user == null ? NotFound() : Ok(user);
        }
        [HttpPost]
        public async Task<ActionResult<GetUserResponseDto>> PostUser(CreateUserRequestDto userDto)
        {
            GetUserResponseDto user = await _userService.AddUser(userDto);
            return user;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            GetUserResponseDto? user = await _userService.DeleteUser(id);
            return user == null ? NotFound() : NoContent();
        }

        [HttpPost("{id}/Cards")]
        public async Task<ActionResult<GetUserCardResponseDto?>> PostUserCard(long id, CreateUserCardRequestDto userCardDto)
        {
            if(userCardDto.UserId != id){ return BadRequest(); }
            GetUserCardResponseDto? userCard = await _userService.AddUserCard(userCardDto);
            return userCard == null ? NotFound() : Ok(userCard);
        }

        [HttpDelete("{id}/Cards/{userCardId}")]
        public async Task<IActionResult> DeleteUserCard(long id, long userCardId)
        {
            GetUserCardResponseDto? userCard = await _userService.DeleteUserCard(id, userCardId);
            return userCard == null ? NotFound() : NoContent();
        }
    }
}