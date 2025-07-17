using CardCollector_backend.Models;

namespace CardCollector_backend.Services.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}