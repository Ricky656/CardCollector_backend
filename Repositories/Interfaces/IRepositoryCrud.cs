using Microsoft.EntityFrameworkCore;

namespace CardCollector_backend.Repositories.Interfaces;

public interface IRepositoryCrud<TModel> where TModel : class
{
    public DbContext Context { get; }
    Task<IEnumerable<TModel>> GetAllAsync();
    Task<TModel?> GetByIdAsync(long id);
    Task CreateAsync(TModel model);
    void Update(TModel model);
    void Delete(TModel model);
    void Delete(long id);
    Task<int> Save();
}