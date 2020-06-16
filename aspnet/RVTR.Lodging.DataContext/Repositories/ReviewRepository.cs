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
  using OrderByFunc = Func<IQueryable<ReviewModel>, IOrderedQueryable<ReviewModel>>;

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
        .Include(x => x.Lodging);//.ThenInclude(x => x.Location).ThenInclude(x => x.Address)
        //.Include(x => x.Lodging).ThenInclude(x => x.Rentals).ThenInclude(x => x.RentalUnit).ThenInclude(x => x.Bathrooms)
        //.Include(x => x.Lodging).ThenInclude(x => x.Rentals).ThenInclude(x => x.RentalUnit).ThenInclude(x => x.Bedrooms)
        //.Include(x => x.Lodging).ThenInclude(x => x.Rentals).ThenInclude(x => x.RentalUnit).ThenInclude(x => x.Images);
    }

    protected override async Task<IEnumerable<ReviewModel>> GetAsync(FilterFuncs filters = null,
                                                          OrderByFunc orderBy = null,
                                                          int resultStartIndex = 0,
                                                          int maxResults = 50)
    {
      var query = IncludeQuery();
      return await this.Select(query, filters, orderBy)
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

    public override async Task<IEnumerable<ReviewModel>> GetAsync(ReviewSearchFilterModel filterModel)
    {
      var filters = GenerateFilterFuncs(filterModel);
      return await GetAsync(filters, null, filterModel.Offset, filterModel.Limit);
    }
  }
}
