using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.DataContext.Repositories
{

  public class RentalRepository : Repository<RentalModel>
  {
    private LodgingContext dbContext;

    public RentalRepository(LodgingContext context) : base(context)
    {
      this.dbContext = context;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="searchFilter"></param>
    /// <param name="maxResults"></param>
    /// <returns></returns>
    public async Task<IEnumerable<RentalModel>> Find(Expression<Func<RentalModel, bool>> searchFilter, int maxResults)
    {
      var rentals = await dbContext.Rentals.
        AsNoTracking().
        Include(x => x.Lodging)
        .Where(searchFilter).ToListAsync();
      // Returns the results asynchronously. Since we are trying to keep things restFul 
      return rentals;
    }
  }
}
