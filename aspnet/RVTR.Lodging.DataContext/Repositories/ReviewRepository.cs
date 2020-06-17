using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.DataContext.Repositories
{

  using FilterFuncs = List<Expression<Func<ReviewModel, bool>>>;
  using OrderByFunc = Expression<Func<ReviewModel, Object>>;

  public class ReviewRepository : Repository<ReviewModel, ReviewSearchFilterModel>
  {
    private LodgingContext dbContext;

    public ReviewRepository(LodgingContext context) : base(context)
    {
      this.dbContext = context;
    }

    private IQueryable<ReviewModel> IncludeQuery()
    {
      return dbContext.Reviews
        .Include(x => x.Lodging);
    }

    protected override async Task<IEnumerable<ReviewModel>> GetAsync(FilterFuncs filters = null,
                                                          OrderByFunc orderBy = null,
                                                          string sortOrder = "asc",
                                                          int resultStartIndex = 0,
                                                          int maxResults = 50)
    {
      var query = IncludeQuery();
      return await this.Select(query, filters, orderBy, sortOrder)
        .AsNoTracking()
        .Skip(resultStartIndex)
        .Take(maxResults)
        .ToListAsync();
    }

    public override async Task<ReviewModel> GetAsync(int id)
    {
      return await IncludeQuery()
        .AsNoTracking()
        .Where(e => e.Id == id)
        .FirstOrDefaultAsync();
    }

    public override async Task<IEnumerable<ReviewModel>> GetAsync(ReviewSearchFilterModel filterModel)
    {
      var filters = GenerateFilterFuncs(filterModel);
      var orderBy = GenerateOrderByFunc(filterModel);
      return await GetAsync(filters, orderBy, filterModel.SortOrder, filterModel.Offset, filterModel.Limit);
    }

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
