using Microsoft.EntityFrameworkCore;
using CardCollector_backend.Repositories.Interfaces;
using CardCollector_backend.Models;

namespace CardCollector_backend.Repositories;

public class PackRepository : RepositoryCrud<Pack>, IPackRepository
{
    public PackRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> PackExists(long id)
    {
        return await _dbSet.AnyAsync(e => e.Id == id);
    }
}