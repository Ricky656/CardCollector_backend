using Microsoft.EntityFrameworkCore;
using CardCollector_backend.Repositories.Interfaces;
using SQLitePCL;

namespace CardCollector_backend.Repositories;

public abstract class RepositoryCrud<TModel> : IRepositoryCrud<TModel> where TModel : class
{
    private readonly DbSet<TModel> _dbSet;
    public DbContext Context { get; }

    protected RepositoryCrud(DbContext context)
    {
        Context = context;
        _dbSet = context.Set<TModel>();
    }

    public virtual async Task<IEnumerable<TModel>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<TModel?> GetByIdAsync(long id)
    {
        return (await _dbSet.FindAsync(id))!;
    }

    public virtual async Task CreateAsync(TModel model)
    {
        await _dbSet.AddAsync(model);

    }

    public virtual void Update(TModel model)
    {
        Context.Entry(model).State = EntityState.Modified;
    }

    public virtual void Delete(TModel model)
    {
        _dbSet.Remove(model);
    }

    public virtual void Delete(long id)
    {
        TModel model = _dbSet.Find(id)!;
        if (model != null)
        {
            Delete(model);
        }
    }

    public async Task<int> Save()
    {
        return await Context.SaveChangesAsync();
    }
}