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

  public class ReviewRepository : Repository<ReviewModel, ReviewQueryParamsModel>
  {
    private readonly LodgingContext dbContext;

    public ReviewRepository(LodgingContext context) : base(context)
    {
      this.dbContext = context;
    }

    /// <summary>
    /// EFCore "Include" functions for including additional data in the query.
    /// </summary>
    /// <param name="queryParams"></param>
    /// <returns></returns>
    private IQueryable<ReviewModel> IncludeQuery()
    {
      return dbContext.Reviews
        .Include(x => x.Lodging);
    }

    /// <summary>
    /// Executes a database query for a specific entity ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="queryParams"></param>
    /// <returns></returns>
    public override async Task<ReviewModel> GetAsync(int id, ReviewQueryParamsModel queryParams)
    {
      return await IncludeQuery()
        .AsNoTracking()
        .Where(e => e.Id == id)
        .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Configures and executes a database query based on query parameters.
    /// </summary>
    /// <param name="queryParams"></param>
    /// <returns></returns>
    public override async Task<IEnumerable<ReviewModel>> GetAsync(ReviewQueryParamsModel queryParams)
    {
      var filters = GenerateFilterFuncs(queryParams);
      var orderBy = GenerateOrderByFunc(queryParams);
      var query = IncludeQuery();
      return await GetAsync(query, filters, orderBy, queryParams.SortOrder, queryParams.Offset, queryParams.Limit);
    }

    /// <summary>
    /// Generates filtering functions based on user-supplied query parameters.
    /// </summary>
    /// <param name="queryParams"></param>
    /// <returns></returns>
    public static FilterFuncs GenerateFilterFuncs(ReviewQueryParamsModel queryParams)
    {
      var filters = new FilterFuncs();
      filters.Add(r => r.Rating >= queryParams.RatingAtLeast);

      if (queryParams.AccountId != null)
      {
        filters.Add(r => r.AccountId == (int)queryParams.AccountId);
      }

      if (queryParams.LodgingId != null)
      {
        filters.Add(r => r.LodgingId == (int)queryParams.LodgingId);
      }

      return filters;
    }

    /// <summary>
    /// Generates ordering functions based on user-supplied data.
    /// </summary>
    /// <param name="queryParams"></param>
    /// <returns></returns>
    public static OrderByFunc GenerateOrderByFunc(ReviewQueryParamsModel queryParams)
    {
      if (!String.IsNullOrEmpty(queryParams.SortKey))
      {
        switch (queryParams.SortKey.ToLower())
        {
          case "id": return (e => e.Id);
          case "accountid": return (e => e.AccountId);
          case "lodgingid": return (e => e.LodgingId);
          case "comment": return (e => e.Comment);
          case "datecreated": return (e => e.DateCreated);
          case "rating": return (e => e.Rating);
        }
      }
      return null;
    }
  }
}
