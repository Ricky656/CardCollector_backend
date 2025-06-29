using CardCollector_backend.Models;
using CardCollector_backend.Repositories.Interfaces;
using Microsoft.AspNetCore.RateLimiting;

namespace CardCollector_backend.Repositories;

public class UserRepository : RepositoryCrud<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public bool UserExists(long id)
    {
        return _dbSet.Any(e => e.Id == id);
    }
}