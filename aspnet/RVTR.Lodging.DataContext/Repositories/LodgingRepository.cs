using System.IO;
using System.Security.AccessControl;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.ObjectModel.Models;
using System.Security.Cryptography.X509Certificates;

namespace RVTR.Lodging.DataContext.Repositories
{
  using FilterFuncs = List<Expression<Func<LodgingModel, bool>>>;
  using OrderByFunc = Func<IQueryable<LodgingModel>, IOrderedQueryable<LodgingModel>>;

  public class LodgingRepository : Repository<LodgingModel, LodgingSearchFilterModel>
  {
    private LodgingContext dbContext;

    public LodgingRepository(LodgingContext context) : base(context)
    {
      this.dbContext = context;
    }

    private IQueryable<LodgingModel> IncludeQuery()
    {
      return dbContext.Lodgings
        .Include(x => x.Location).ThenInclude(x => x.Address)
        .Include(x => x.Rentals).ThenInclude(x => x.RentalUnit).ThenInclude(x => x.Bathrooms)
        .Include(x => x.Rentals).ThenInclude(x => x.RentalUnit).ThenInclude(x => x.Bedrooms)
        .Include(x => x.Rentals).ThenInclude(x => x.RentalUnit).ThenInclude(x => x.Images)
        .Include(x => x.Reviews);
    }

    protected override async Task<IEnumerable<LodgingModel>> GetAsync(FilterFuncs filters = null,
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

    public override async Task<LodgingModel> GetAsync(int id)
    {
      return await IncludeQuery()
        .AsNoTracking()
        .Where(e => e.Id == id)
        .FirstOrDefaultAsync();
    }

    private FilterFuncs GenerateFilterFuncs(LodgingSearchFilterModel filterModel)
    {
      var filters = new FilterFuncs();
      filters.Add(m => Math.Round(m.Reviews.Average(r => r.Rating)) >= filterModel.RatingAtLeast);
      filters.Add(m => m.Rentals.Sum(r => r.RentalUnit.Bedrooms.Count()) >= filterModel.BedsAtLeast);
      filters.Add(m => m.Rentals.Sum(r => r.RentalUnit.Bathrooms.Count()) >= filterModel.BathsAtLeast);
      return filters;
    }

    private OrderByFunc GenerateOrderByFunc(LodgingSearchFilterModel filterModel)
    {
      return (t => t.OrderByDescending(f => f.Id));
    }

    public override async Task<IEnumerable<LodgingModel>> GetAsync(LodgingSearchFilterModel filterModel)
    {
      var filters = GenerateFilterFuncs(filterModel);
      return await GetAsync(filters, null, filterModel.Paginate, filterModel.Limit);
    }
  }
}
