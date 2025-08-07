using CardCollector_backend.Models;
using CardCollector_backend.Repositories.Interfaces;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace CardCollector_backend.Repositories;

public class UserRepository : RepositoryCrud<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> UserExists(long id)
    {
        return await _dbSet.AnyAsync(e => e.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }
}