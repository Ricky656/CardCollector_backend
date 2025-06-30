using CardCollector_backend.Models;

namespace CardCollector_backend.Repositories.Interfaces;

public interface IUserCardRepository : IRepositoryCrud<UserCard>
{
    Task<bool> UserCardExists(long id);
    Task<IEnumerable<UserCard>> GetAllByIdAsync(long id);
}