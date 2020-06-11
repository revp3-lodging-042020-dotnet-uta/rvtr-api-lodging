using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.DataContext.Repositories
{
  /// <summary>
  /// Represents the _Repository_ generic
  /// </summary>
  /// <typeparam name="TEntity"></typeparam>
  public class Repository<TEntity> where TEntity : class
  {
    public readonly DbSet<TEntity> _db;

    public Repository(LodgingContext context)
    {
      _db = context.Set<TEntity>();
    }

    public virtual async Task<TEntity> DeleteAsync(int id) {
        var entity = await GetAsync(id);
        _db.Remove(entity);
        return entity;
    }

    public virtual async Task<TEntity> InsertAsync(TEntity entry) {
        return (await _db.AddAsync(entry).ConfigureAwait(true)).Entity;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync() => await _db.ToListAsync();

    public virtual async Task<TEntity> GetAsync(int id) => await _db.FindAsync(id).ConfigureAwait(true);

    public virtual TEntity Update(TEntity entry) {
        _db.Update(entry);
        return entry;
    }
  }
}
