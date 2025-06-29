using CardCollector_backend.Models;
using CardCollector_backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardCollector_backend.Repositories;

public class UserCardRepository : RepositoryCrud<UserCard>, IUserCardRepository
{
    public UserCardRepository(AppDbContext context) : base(context)
    {
    }

    public bool UserCardExists(long id)
    {
        return _dbSet.Any(e => e.Id == id);
    }

    public async Task<IEnumerable<UserCard>> GetAllByIdAsync(long userId)
    {
        return await _dbSet
            .Include("Users")
            .Where(u => u.Id == userId)
            .ToListAsync();
    }
}