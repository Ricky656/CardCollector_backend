using CardCollector_backend.Models;
using CardCollector_backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardCollector_backend.Repositories;

public class UserCardRepository : RepositoryCrud<UserCard>, IUserCardRepository
{
    //UserCards are set to always include associated Card object via Entity Framework's model builder options
    public UserCardRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> UserCardExists(long id)
    {
        return await _dbSet.AnyAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<UserCard>> GetAllByIdAsync(long userId)
    {
        return await _dbSet
            .Where(u => u.UserId == userId)
            .ToListAsync();
    }
}