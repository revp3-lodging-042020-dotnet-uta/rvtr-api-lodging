using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.DataContext.Repositories
{

  /// <summary>
  /// Function to be applied for results filtering.
  /// </summary>
  using FilterFuncs = List<Expression<Func<ReviewModel, bool>>>;

  /// <summary>
  /// Function to be applied for result ordering.
  /// </summary>
  using OrderByFunc = Expression<Func<ReviewModel, Object>>;

  public class ReviewRepository : Repository<ReviewModel, ReviewSearchFilterModel>
  {
    private LodgingContext dbContext;

    public ReviewRepository(LodgingContext context) : base(context)
    {
      this.dbContext = context;
    }

    /// <summary>
    /// EFCore "Include" functions for including additional data in the query.
    /// </summary>
    /// <param name="filterModel"></param>
    /// <returns></returns>
    private IQueryable<ReviewModel> IncludeQuery(ReviewSearchFilterModel filterModel)
    {
      return dbContext.Reviews
        .Include(x => x.Lodging);
    }

    /// <summary>
    /// Executes a database query for all entities based on filtering parameters.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="filters"></param>
    /// <param name="orderBy"></param>
    /// <param name="sortOrder"></param>
    /// <param name="resultOffset">start results from this value</param>
    /// <param name="maxResults"></param>
    /// <returns></returns>
    protected override async Task<IEnumerable<ReviewModel>> GetAsync(IQueryable<ReviewModel> query,
                                                                     FilterFuncs filters = null,
                                                                     OrderByFunc orderBy = null,
                                                                     string sortOrder = "asc",
                                                                     int resultStartIndex = 0,
                                                                     int maxResults = 50)
    {
      return await this.Select(query, filters, orderBy, sortOrder)
        .AsNoTracking()
        .Skip(resultStartIndex)
        .Take(maxResults)
        .ToListAsync();
    }

    /// <summary>
    /// Executes a database query for a specific entity ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="filterModel"></param>
    /// <returns></returns>
    public override async Task<ReviewModel> GetAsync(int id, ReviewSearchFilterModel filterModel)
    {
      return await IncludeQuery(filterModel)
        .AsNoTracking()
        .Where(e => e.Id == id)
        .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Configures an executes a database query based on filtering parameters.
    /// </summary>
    /// <param name="filterModel"></param>
    /// <returns></returns>
    public override async Task<IEnumerable<ReviewModel>> GetAsync(ReviewSearchFilterModel filterModel)
    {
      var filters = GenerateFilterFuncs(filterModel);
      var orderBy = GenerateOrderByFunc(filterModel);
      var query = IncludeQuery(filterModel);
      return await GetAsync(query, filters, orderBy, filterModel.SortOrder, filterModel.Offset, filterModel.Limit);
    }

    /// <summary>
    /// Generates filtering functions based on user-supplied filtering parameters.
    /// </summary>
    /// <param name="filterModel"></param>
    /// <returns></returns>
    private FilterFuncs GenerateFilterFuncs(ReviewSearchFilterModel filterModel)
    {
      var filters = new FilterFuncs();
      filters.Add(r => r.Rating >= filterModel.RatingAtLeast);

      if (filterModel.AccountId != null)
      {
        filters.Add(r => r.AccountId == (int)filterModel.AccountId);
      }

      if (filterModel.LodgingId != null)
      {
        filters.Add(r => r.LodgingId == (int)filterModel.LodgingId);
      }

      return filters;
    }

    /// <summary>
    /// Generates ordering functions based on user-supplied data.
    /// </summary>
    /// <param name="filterModel"></param>
    /// <returns></returns>
    private OrderByFunc GenerateOrderByFunc(ReviewSearchFilterModel filterModel)
    {
      if (!String.IsNullOrEmpty(filterModel.SortKey))
      {
        switch (filterModel.SortKey)
        {
          case "Id": return (e => e.Id);
          case "AccountId": return (e => e.AccountId);
          case "Comment": return (e => e.Comment);
          case "DateCreated": return (e => e.DateCreated);
          case "Rating": return (e => e.Rating);
        }
      }
      return null;
    }
  }
}
