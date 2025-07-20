using CardCollector_backend.Models;
using CardCollector_backend.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CardCollector_backend.Services;
public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    public TokenService(IConfiguration configuration)
    {
        _config = configuration;
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
}