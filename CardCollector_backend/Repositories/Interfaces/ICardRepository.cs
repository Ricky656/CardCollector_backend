using CardCollector_backend.Models;

namespace CardCollector_backend.Repositories.Interfaces;

public interface ICardRepository : IRepositoryCrud<Card>
{
    Task<bool> CardExists(long id);
}