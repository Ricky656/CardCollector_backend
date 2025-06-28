using Microsoft.EntityFrameworkCore;
using CardCollector_backend.Repositories;
using CardCollector_backend.Repositories.Interfaces;
using CardCollector_backend.Models;
using Microsoft.Identity.Client;

namespace CardCollector_backend.Repositories;

public class CardRepository : RepositoryCrud<Card>, ICardRepository
{
    public CardRepository(AppDbContext context) : base(context)
    {
    }

    public bool CardExists(long id)
    {
        return _dbSet.Any(e => e.Id == id);
    }
}