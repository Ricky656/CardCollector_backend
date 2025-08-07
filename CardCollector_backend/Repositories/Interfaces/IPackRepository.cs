using CardCollector_backend.Models;

namespace CardCollector_backend.Repositories.Interfaces;

public interface IPackRepository : IRepositoryCrud<Pack>
{
    Task<bool> PackExists(long Id);
}