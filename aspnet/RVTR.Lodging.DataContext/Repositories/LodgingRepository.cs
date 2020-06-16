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
  using FilterFunc = Expression<Func<LodgingModel, bool>>;
  using OrderByFunc = Func<IQueryable<LodgingModel>, IOrderedQueryable<LodgingModel>>;

  public class LodgingRepository : Repository<LodgingModel>
  {
    private LodgingContext dbContext;

    public LodgingRepository(LodgingContext context) : base(context)
    {
      this.dbContext = context;
    }

    private void GenerateSearchFilter(SearchFilterModel searchFilter)
    {

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

    public override async Task<IEnumerable<LodgingModel>> GetAsync(FilterFunc filter = null,
                                                          OrderByFunc orderBy = null,
                                                          int resultStartIndex = 0,
                                                          int maxResults = 50)
    {
      var query = IncludeQuery();
      return await this.Select(query, filter, orderBy)
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
  }
}
