using Microsoft.AspNetCore.Mvc;
using CardCollector_backend.Services.Interfaces;
using CardCollector_backend.Dtos.Users;

namespace CardCollector_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService service)
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
    }
}