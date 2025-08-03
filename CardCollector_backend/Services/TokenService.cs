using CardCollector_backend.Models;
using CardCollector_backend.Services.Interfaces;
using CardCollector_backend.Dtos.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CardCollector_backend.Repositories.Interfaces;

namespace CardCollector_backend.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly IUserRepository _userRepo;
    public TokenService(IConfiguration configuration, IUserRepository userRepository)
    {
        _config = configuration;
        _userRepo = userRepository;
    }
    public string CreateToken(User user)
    {
        List<Claim> claims =
        [
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        ];

        SymmetricSecurityKey key = new(
            Encoding.UTF8.GetBytes(_config.GetValue<string>("AppSettings:Secret")!)
        );

        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512);

        JwtSecurityToken token = new(
            issuer: _config.GetValue<string>("AppSettings:Issuer"),
            audience: _config.GetValue<string>("AppSettings:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_config.GetValue<double>("AppSettings:TokenValidityMinutes")),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string CreateRefreshToken()
    {
        Byte[] ran = new byte[32];
        using RandomNumberGenerator rng = RandomNumberGenerator.Create();
        rng.GetBytes(ran);
        return Convert.ToBase64String(ran);
    }

    public async Task<LoginResponseUserDto> CreateUserTokens(User user, HttpContext context)
    {
        LoginResponseUserDto loginDto = new()
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role,
            Token = CreateToken(user),
        };
        string refreshToken = CreateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpirey = DateTime.UtcNow.AddDays(_config.GetValue<double>("AppSettings:RefreshValidityDays"));

        context.Response.Cookies.Delete("refreshToken");
        context.Response.Cookies.Append("refreshToken", refreshToken,
            new CookieOptions
            {
                Expires = user.RefreshTokenExpirey,
                HttpOnly = true,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });

        await _userRepo.Update(user);
        return loginDto;
    }
}