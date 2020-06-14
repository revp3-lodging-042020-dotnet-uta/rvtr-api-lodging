using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.DataContext;
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
    /// <param name="searchFilter"></param>
    /// <param name="maxResults"></param>
    /// <returns></returns>
    public async Task<IEnumerable<LodgingModel>> Find(Expression<Func<LodgingModel, bool>> searchFilter,int maxResults)
    {
      var lodgings = await dbcontext.Lodgings
        .AsNoTracking()
        .Include(x => x.Rentals).ThenInclude(x => x.RentalUnit).ThenInclude(x => x.Bathrooms)
        .Include(x => x.Rentals).ThenInclude(x => x.RentalUnit).ThenInclude(x => x.Bedrooms)
        .Include(x => x.Reviews).Include(x => x.Location).ThenInclude(x => x.Address)
        .Where(searchFilter).Take(maxResults).ToListAsync();
      // Returns the results asynchronously. Since we are trying to keep things restFul 
      return lodgings;
    }

    public async Task<IEnumerable<LodgingModel>> OrderBy(List<LodgingModel> lodgings,string param)
    {
      var orderedLodging = lodgings;
      switch (param)
      {
        case "Desc":
           orderedLodging= await lodgings.OrderByDescending(x=>x.Reviews).AsQueryable().ToListAsync();
           break;
        case "Asc":
          orderedLodging = await lodgings.OrderBy(x => x.Reviews).AsQueryable().ToListAsync();
          break;
        default:
          orderedLodging = await lodgings.OrderBy(x => x.Name).AsQueryable().ToListAsync();
          break;
      } ;
      return orderedLodging;
    }

    public  async Task<IEnumerable<LodgingModel>> GetLodges(QueryClass query)
    {
     var lodgings=  await Find(x=>x.Name == query.Search, query.Limit);
     var lodgeOrder = await OrderBy(lodgings.ToList(),query.SortKey );
     return lodgeOrder;
    }
  }
}
