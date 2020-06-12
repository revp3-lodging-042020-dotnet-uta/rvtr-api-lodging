using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.DataContext.Repositories
{

  using FilterFunc = Expression<Func<RentalModel, bool>>;

  public class RentalRepository : Repository<RentalModel>
  {
    private LodgingContext dbContext;


    public RentalRepository(LodgingContext context) : base(context)
    {
      this.dbContext = context;
    }

    public async Task<IEnumerable<RentalModel>> Find(FilterFunc searchFilter,
                                                     int maxResults)
    {
      var rentals = await dbContext.Rentals
        .AsNoTracking()
        .Include(x => x.Lodging)
        .Include(x => x.RentalUnit).ThenInclude(x => x.Bathrooms)
        .Include(x => x.RentalUnit).ThenInclude(x => x.Bedrooms)
        .Include(x => x.RentalUnit).ThenInclude(x => x.Images)
        .Where(searchFilter)
        .Take(maxResults)
        .ToListAsync();

      return rentals;
    }
  }
}
