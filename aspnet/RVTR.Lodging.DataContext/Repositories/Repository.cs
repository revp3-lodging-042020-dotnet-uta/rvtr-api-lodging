using System.Threading;
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
  public abstract class Repository<TEntity, TSearchFilterModel> where TEntity : class where TSearchFilterModel : class
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

    public abstract Task<IEnumerable<TEntity>> GetAsync(TSearchFilterModel filterModel);

    public virtual async Task<TEntity> GetAsync(int id) => await _db.FindAsync(id).ConfigureAwait(true);

    public virtual TEntity Update(TEntity entry) {
      _db.Update(entry);
      return entry;
    }

    protected virtual IQueryable<TEntity> Select(IQueryable<TEntity> query,
                                                 List<Expression<Func<TEntity, bool>>> filters = null,
                                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
    {
      foreach(var filter in filters)
      {
        query = query.Where(filter);
      }

      if (orderBy != null) return orderBy(query);

      return query;
    }

    protected virtual async Task<IEnumerable<TEntity>> GetAsync(List<Expression<Func<TEntity, bool>>> filters = null,
                                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                                int resultStartIndex = 0,
                                                                int maxResults = 50)
    {
      return await this.Select(this._db, filters, orderBy)
        .AsNoTracking()
        .Skip(resultStartIndex)
        .Take(maxResults)
        .ToListAsync();
    }
  }
}
