using Microsoft.EntityFrameworkCore;
using CardCollector_backend.Repositories.Interfaces;

namespace CardCollector_backend.Repositories;

public abstract class RepositoryCrud<TModel> : IRepositoryCrud<TModel> where TModel : class
{
    public DbSet<TModel> _dbSet { get; }
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

    public virtual async Task<TModel> CreateAsync(TModel model)
    {
        await _dbSet.AddAsync(model);
        await Context.SaveChangesAsync();
        return model;

    }

    public virtual async Task<TModel?> Update(TModel model)
    {
        //Context.Entry(model).State = EntityState.Modified;
        _dbSet.Update(model);
        await Context.SaveChangesAsync();
        return model;
    }

    public virtual async Task<TModel?> Delete(TModel model)
    {
        _dbSet.Remove(model);
        await Context.SaveChangesAsync();
        return model;
    }

    public virtual async Task<TModel?> Delete(long id)
    {
        var model = await _dbSet.FindAsync(id);
        if (model != null)
        {
            model = await Delete(model);
        }
        return model;
    }

    /*public async Task<TModel?> Save()
    {
        return await Context.SaveChangesAsync();
    }*/
}