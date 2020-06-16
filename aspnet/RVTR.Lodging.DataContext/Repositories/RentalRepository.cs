using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.DataContext.Repositories
{

  using FilterFuncs = List<Expression<Func<RentalModel, bool>>>;
  using OrderByFunc = Func<IQueryable<RentalModel>, IOrderedQueryable<RentalModel>>;

  public class RentalRepository : Repository<RentalModel, RentalSearchFilterModel>
  {
    private LodgingContext dbContext;


    public RentalRepository(LodgingContext context) : base(context)
    {
      this.dbContext = context;
    }

    private IQueryable<RentalModel> IncludeQuery()
    {
      return dbContext.Rentals
        .Include(x => x.Lodging)
        .Include(x => x.RentalUnit).ThenInclude(x => x.Bathrooms)
        .Include(x => x.RentalUnit).ThenInclude(x => x.Bedrooms)
        .Include(x => x.RentalUnit).ThenInclude(x => x.Images);
    }

    protected override async Task<IEnumerable<RentalModel>> GetAsync(FilterFuncs filters = null,
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

    public override async Task<RentalModel> GetAsync(int id)
    {
      return await IncludeQuery()
        .AsNoTracking()
        .Where(e => e.Id == id)
        .FirstOrDefaultAsync();
    }

    private FilterFuncs GenerateFilterFuncs(RentalSearchFilterModel filterModel)
    {
      var filters = new FilterFuncs();
      filters.Add(r => r.RentalUnit.Bedrooms.Count() >= filterModel.BedsAtLeast);
      filters.Add(r => r.RentalUnit.Bathrooms.Count() >= filterModel.BathsAtLeast);

      return filters;
    }

    public override async Task<IEnumerable<RentalModel>> GetAsync(RentalSearchFilterModel filterModel)
    {
      var filters = GenerateFilterFuncs(filterModel);
      return await GetAsync(filters, null, filterModel.Offset, filterModel.Limit);
    }
  }
}
