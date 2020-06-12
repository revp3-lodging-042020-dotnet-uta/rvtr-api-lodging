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

    public class LodgingRepository : Repository<LodgingModel>
    {
        private LodgingContext dbContext;

        public LodgingRepository(LodgingContext context) : base(context)
        {
          this.dbContext = context;
        }

        public override async Task<IEnumerable<LodgingModel>> GetAsync()
        {
          return await dbContext.Lodgings
            .AsNoTracking()
            .Include(x => x.Location).ThenInclude(x => x.Address)
            .Include(x => x.Rentals).ThenInclude(x => x.RentalUnit).ThenInclude(x => x.Bathrooms)
            .Include(x => x.Rentals).ThenInclude(x => x.RentalUnit).ThenInclude(x => x.Bedrooms)
            .Include(x => x.Rentals).ThenInclude(x => x.RentalUnit).ThenInclude(x => x.Images)
            .Include(x => x.Reviews)
            .ToListAsync();
        }

        public override async Task<LodgingModel> GetAsync(int id)
        {
          return await dbContext.Lodgings
            .AsNoTracking()
            .Include(x => x.Location).ThenInclude(x => x.Address)
            .Include(x => x.Rentals).ThenInclude(x => x.RentalUnit).ThenInclude(x => x.Bathrooms)
            .Include(x => x.Rentals).ThenInclude(x => x.RentalUnit).ThenInclude(x => x.Bedrooms)
            .Include(x => x.Rentals).ThenInclude(x => x.RentalUnit).ThenInclude(x => x.Images)
            .Include(x => x.Reviews)
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<LodgingModel>> Find(FilterFunc searchFilter,
                                                              int maxResults)
        {
          var lodgings = await dbContext.Lodgings
            .AsNoTracking()
            .Include(x => x.Location).ThenInclude(x => x.Address)
            .Include(x => x.Rentals).ThenInclude(x => x.RentalUnit).ThenInclude(x => x.Bathrooms)
            .Include(x => x.Rentals).ThenInclude(x => x.RentalUnit).ThenInclude(x => x.Bedrooms)
            .Include(x => x.Rentals).ThenInclude(x => x.RentalUnit).ThenInclude(x => x.Images)
            .Include(x => x.Reviews)
            .Where(searchFilter)
            .Take(maxResults)
            .ToListAsync();

          return lodgings;
        }
    }
}
