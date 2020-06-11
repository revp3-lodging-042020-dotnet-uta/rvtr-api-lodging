using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.DataContext.Repositories
{
  public class LodgingRepository : Repository<LodgingModel>
  {
    private LodgingContext dbcontext;

    public LodgingRepository(LodgingContext context) : base(context)
    {
      this.dbcontext = context;
    }
    /// <summary>
    /// The way Im trying this I've removed Override for now because there isn't a find to override
    /// Keeping AsNoTracking because while there should be any changes made through this call we don't want to track
    /// it just in case
    /// After that since were doing the EF Core method we need to include any connected tables,From what I can tell that means
    /// Rentals, Location,Reviews
    /// Then add a where clause that checks the predicate, This should
    /// I use x in lamdas across the board because I find it to be more intuitive.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public async Task<IEnumerable<LodgingModel>> Find(Expression<Func<LodgingModel, bool>> predicate,int resultsNum)
    {

      var lodgings = await dbcontext.Lodgings
        .AsNoTracking().Include(x => x.Rentals)
        .Include(x => x.Reviews).Include(x => x.Location)
        .Where(predicate).ToListAsync();
      var topResults =  lodgings.Take(resultsNum);

      // Returns the results asynchronously. Since we are trying to keep things restFul 
      return topResults;
    }
  }
}
