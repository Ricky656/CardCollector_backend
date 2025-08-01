using CardCollector_backend.Dtos.Users;
using CardCollector_backend.Models;

namespace CardCollector_backend.Services.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
    string CreateRefreshToken();
    Task<LoginResponseUserDto> CreateUserTokens(User user, HttpContext context);
}