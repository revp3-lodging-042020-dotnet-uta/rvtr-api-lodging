using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.DataContext.Repositories
{

  using FilterFunc = Expression<Func<ReviewModel, bool>>;
  using OrderByFunc = Func<IQueryable<ReviewModel>, IOrderedQueryable<ReviewModel>>;

  public class ReviewRepository : Repository<ReviewModel>
  {
    private LodgingContext dbContext;

    public ReviewRepository(LodgingContext context) : base(context)
    {
      this.dbContext = context;
    }

    private IQueryable<ReviewModel> IncludeQuery()
    {
      return dbContext.Reviews
        .Include(x => x.Lodging).ThenInclude(x => x.Location).ThenInclude(x => x.Address)
        .Include(x => x.Lodging).ThenInclude(x => x.Rentals).ThenInclude(x => x.RentalUnit).ThenInclude(x => x.Bathrooms)
        .Include(x => x.Lodging).ThenInclude(x => x.Rentals).ThenInclude(x => x.RentalUnit).ThenInclude(x => x.Bedrooms)
        .Include(x => x.Lodging).ThenInclude(x => x.Rentals).ThenInclude(x => x.RentalUnit).ThenInclude(x => x.Images);
    }

    public override async Task<IEnumerable<ReviewModel>> GetAsync(FilterFunc filter = null,
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

    public override async Task<ReviewModel> GetAsync(int id)
    {
      return await IncludeQuery()
        .AsNoTracking()
        .Where(e => e.Id == id)
        .FirstOrDefaultAsync();
    }
  }
}
