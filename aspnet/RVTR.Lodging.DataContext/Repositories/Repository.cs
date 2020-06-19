using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RVTR.Lodging.DataContext.Repositories
{
  /// <summary>
  /// Repository class to be used as a base for concrete repositories.
  /// </summary>
  /// <typeparam name="TEntity">The entity the repository is responsible for handling</typeparam>
  /// <typeparam name="TQueryParamsModel">The query parameter model used for processing query string</typeparam>
  public abstract class Repository<TEntity, TQueryParamsModel> where TEntity : class where TQueryParamsModel : class
  {
    public readonly DbSet<TEntity> _db;

    protected Repository(LodgingContext context)
    {
      _db = context.Set<TEntity>();
    }

    /// <summary>
    /// Delete an entity by id.
    /// </summary>
    /// <param name="id">ID of the entity</param>
    /// <param name="queryParams">Query parameter model</param>
    public virtual async Task<TEntity> DeleteAsync(int id, TQueryParamsModel queryParams) {
      var entity = await GetAsync(id, queryParams);
      _db.Remove(entity);
      return entity;
    }

    /// <summary>
    /// Insert a new entity.
    /// </summary>
    /// <param name="entry">The entity to be inserted.</param>
    public virtual async Task<TEntity> InsertAsync(TEntity entry) {
      return (await _db.AddAsync(entry).ConfigureAwait(true)).Entity;
    }

    /// <summary>
    /// Gets an entity by it's ID.
    /// </summary>
    public virtual async Task<TEntity> GetAsync(int id, TQueryParamsModel queryParams) => await _db.FindAsync(id).ConfigureAwait(true);

    /// <summary>
    /// Gets all entities based on the supplied query parameters
    /// </summary>
    /// <param name="queryParams">Query parameter model</param>
    /// <returns></returns>
    public abstract Task<IEnumerable<TEntity>> GetAsync(TQueryParamsModel queryParams);

    /// <summary>
    /// Executes a composed query.
    /// </summary>
    /// <param name="query">A queryable with .Include methods already added</param>
    /// <param name="filters">Filter functions to be applied to the query</param>
    /// <param name="orderBy">Ordering function to be applied to the query</param>
    /// <param name="sortOrder">The sort order ("asc" or "desc")</param>
    /// <param name="resultOffset">Return results starting from this index</param>
    /// <param name="maxResults">Maximum number of results to return</param>
    protected virtual async Task<IEnumerable<TEntity>> GetAsync(IQueryable<TEntity> query = null,
                                                                List<Expression<Func<TEntity, bool>>> filters = null,
                                                                Expression<Func<TEntity, Object>> orderBy = null,
                                                                string sortOrder = "asc",
                                                                int resultOffset = 0,
                                                                int maxResults = 50)
    {
      return await this.Apply(query, filters, orderBy, sortOrder)
        .AsNoTracking()
        .Skip(resultOffset)
        .Take(maxResults)
        .ToListAsync();
    }


    /// <summary>
    /// Applies the provided filters and ordering to a query.
    /// </summary>
    /// <param name="query">The query to apply filtering and ordering to</param>
    /// <param name="filters">Filter functions to be applied</param>
    /// <param name="orderBy">Ordering function to be applied</param>
    /// <param name="sortOrder">The sort order ("asc" or "desc"></param>
    protected IQueryable<TEntity> Apply(IQueryable<TEntity> query,
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

    /// <summary>
    /// Updates the entity with new data.
    /// </summary>
    public virtual TEntity Update(TEntity entry) {
      _db.Update(entry);
      return entry;
    }

  }
}
