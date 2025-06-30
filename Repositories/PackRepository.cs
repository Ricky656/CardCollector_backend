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

    public override async Task<Pack?> GetByIdAsync(long id)
    {
        //return base.GetByIdAsync(id);
        return await _dbSet.Include("Cards").FirstOrDefaultAsync(e => e.Id == id);
    }

    public override async Task<Pack?> Update(Pack updatedPack)
    {
        Pack? pack = await _dbSet.Include("Cards").FirstOrDefaultAsync(e => e.Id == updatedPack.Id);
        if (pack == null) { return null; }
        pack.Name = updatedPack.Name;
        foreach (Card card in pack.Cards)
        {
            if (!updatedPack.Cards.Remove(card))
            {
                pack.Cards.Remove(card);
            }
        }
        foreach (Card newCard in updatedPack.Cards)
        {
            pack.Cards.Add(newCard);
        }
        await Context.SaveChangesAsync();
        return pack;
    }
}