using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

    public virtual async Task<TEntity> DeleteAsync(int id, TSearchFilterModel filterModel) {
      var entity = await GetAsync(id, filterModel);
      _db.Remove(entity);
      return entity;
    }

    public virtual async Task<TEntity> InsertAsync(TEntity entry) {
      return (await _db.AddAsync(entry).ConfigureAwait(true)).Entity;
    }

    public virtual async Task<TEntity> GetAsync(int id, TSearchFilterModel filterModel) => await _db.FindAsync(id).ConfigureAwait(true);

    public abstract Task<IEnumerable<TEntity>> GetAsync(TSearchFilterModel filterModel);

    protected virtual async Task<IEnumerable<TEntity>> GetAsync(IQueryable<TEntity> query = null,
                                                                List<Expression<Func<TEntity, bool>>> filters = null,
                                                                Expression<Func<TEntity, Object>> orderBy = null,
                                                                string sortOrder = "asc",
                                                                int resultOffset = 0,
                                                                int maxResults = 50)
    {
      return await this.Select(this._db, filters, orderBy, sortOrder)
        .AsNoTracking()
        .Skip(resultOffset)
        .Take(maxResults)
        .ToListAsync();
    }

    public virtual TEntity Update(TEntity entry) {
      _db.Update(entry);
      return entry;
    }

    protected virtual IQueryable<TEntity> Select(IQueryable<TEntity> query,
                                                 List<Expression<Func<TEntity, bool>>> filters = null,
                                                 Expression<Func<TEntity, Object>> orderBy = null,
                                                 string sortOrder = "asc")
    {
      foreach(var filter in filters)
      {
        query = query.Where(filter);
      }

      if (orderBy != null)
      {
        switch (sortOrder)
        {
          case "asc": return query.OrderBy(orderBy);
          case "desc": return query.OrderByDescending(orderBy);
          default: return query.OrderBy(orderBy);
        }
      }

      return query;
    }

  }
}
