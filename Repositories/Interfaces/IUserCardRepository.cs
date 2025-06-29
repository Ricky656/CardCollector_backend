using CardCollector_backend.Models;

namespace CardCollector_backend.Repositories.Interfaces;

public interface IUserCardRepository : IRepositoryCrud<UserCard>
{
    bool UserCardExists(long id);
    Task<IEnumerable<UserCard>> GetAllByIdAsync(long id);
}