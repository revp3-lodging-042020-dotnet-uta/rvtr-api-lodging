using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

    public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
                                                             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                             int resultStartIndex = 0,
                                                             int maxResults = 50)
    {
      return await this.Select(this._db, filter, orderBy)
        .AsNoTracking()
        .Skip(resultStartIndex)
        .Take(maxResults)
        .ToListAsync();
    }

    public virtual async Task<TEntity> GetAsync(int id) => await _db.FindAsync(id).ConfigureAwait(true);

    public virtual TEntity Update(TEntity entry) {
      _db.Update(entry);
      return entry;
    }

    protected virtual IQueryable<TEntity> Select(IQueryable<TEntity> query,
                                                 Expression<Func<TEntity, bool>> filter = null,
                                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
    {
      if (filter != null) query = query.Where(filter);

      if (orderBy != null) return orderBy(query);

      return query;
    }
  }
}
