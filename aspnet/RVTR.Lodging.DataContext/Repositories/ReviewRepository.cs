using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.DataContext.Repositories
{
  public class ReviewRepository : Repository<ReviewModel>
  {
    private LodgingContext dbcontext;

    public ReviewRepository(LodgingContext context) : base(context)
    {
      this.dbcontext = context;
    }
    /// <summary>
    /// Filtered and searched  Reviews
    /// </summary>
    /// <param name="searchFilter"></param>
    /// <param name="maxResults"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ReviewModel>> Find(Expression<Func<ReviewModel, bool>> searchFilter, int maxResults)
    {
      var lodgings = await dbcontext.Reviews
        .AsNoTracking().Include(x=>x.Lodging).ThenInclude(x=>x.Id)
        .Where(searchFilter).Take(maxResults).ToListAsync();
      // Returns the results asynchronously. Since we are trying to keep things restFul 
      return lodgings;
    }
  }
}
