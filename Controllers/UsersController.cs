using Microsoft.AspNetCore.Mvc;
using CardCollector_backend.Services.Interfaces;
using CardCollector_backend.Dtos.Users;
using CardCollector_backend.Dtos.UserCards;
using CardCollector_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

        [Authorize(Roles ="Admin")]
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

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseUserDto>> Login(LoginUserDto userDto)
        {
            LoginResponseUserDto? login = await _userService.Login(userDto, HttpContext);
            return login == null ? BadRequest("Email or password are wrong!") : Ok(login);
        }
        [HttpPost("refresh")]
        public async Task<ActionResult<LoginResponseUserDto?>> RefreshLogin(RefreshLoginDto refreshDto)
        {
            HttpContext.Request.Cookies.TryGetValue("refreshToken", out string? refreshToken);
            refreshDto.RefreshToken = refreshToken;
            LoginResponseUserDto? responseDto = await _userService.RefreshLogin(refreshDto, HttpContext);
            return responseDto == null ? Unauthorized("Invalid refresh token") : responseDto;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("admin")]
        public ActionResult CheckAdmin()
        {
            return Ok();
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

        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            GetUserResponseDto? user = await _userService.DeleteUser(id);
            return user == null ? NotFound() : NoContent();
        }

        [HttpPost("{id}/Cards")]
        public async Task<ActionResult<GetUserCardResponseDto?>> PostUserCard(long id, CreateUserCardRequestDto userCardDto)
        {
            if (userCardDto.UserId != id) { return BadRequest(); }
            GetUserCardResponseDto? userCard = await _userService.AddUserCard(userCardDto);
            return userCard == null ? NotFound() : Ok(userCard);
        }

        [HttpDelete("{id}/Cards/{userCardId}")]
        public async Task<IActionResult> DeleteUserCard(long id, long userCardId)
        {
            GetUserCardResponseDto? userCard = await _userService.DeleteUserCard(id, userCardId);
            return userCard == null ? NotFound() : NoContent();
        }

        [Authorize]
        [HttpPost("{id}/Packs/{packId}")]
        public async Task<ActionResult<IEnumerable<GetUserCardResponseDto>?>> OpenPack(long id, long packId)
        {
            IEnumerable<GetUserCardResponseDto>? userCards = await _userService.OpenPack(id, packId);
            return userCards == null ? NotFound() : Ok(userCards);
        }
    }
}