using Microsoft.EntityFrameworkCore;

namespace CardCollector_backend.Repositories.Interfaces;

public interface IRepositoryCrud<TModel> where TModel : class
{
    public DbContext Context { get; }
    public DbSet<TModel> _dbSet { get;}
    Task<IEnumerable<TModel>> GetAllAsync();
    Task<TModel?> GetByIdAsync(long id);
    Task<TModel> CreateAsync(TModel model);
    Task<TModel?> Update(TModel model);
    Task<TModel?> Delete(TModel model);
    Task<TModel?> Delete(long id);
    //Task<TModel?> Save();
}