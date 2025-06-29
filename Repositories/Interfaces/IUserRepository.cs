using CardCollector_backend.Models;

namespace CardCollector_backend.Repositories.Interfaces;

public interface IUserRepository : IRepositoryCrud<User>
{
    bool UserExists(long id);
}