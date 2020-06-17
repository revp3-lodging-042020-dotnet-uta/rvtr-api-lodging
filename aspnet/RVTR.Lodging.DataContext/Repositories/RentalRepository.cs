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
  using OrderByFunc = Expression<Func<RentalModel, Object>>;

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
        .Include(x => x.RentalUnit).ThenInclude(x => x.Bedrooms).ThenInclude(x => x.Images)
        .Include(x => x.RentalUnit).ThenInclude(x => x.Bedrooms).ThenInclude(x => x.Amenities);
    }

    protected override async Task<IEnumerable<RentalModel>> GetAsync(FilterFuncs filters = null,
                                                          OrderByFunc orderBy = null,
                                                          string sortOrder = "asc",
                                                          int resultStartIndex = 0,
                                                          int maxResults = 50)
    {
      var query = IncludeQuery();
      return await this.Select(query, filters, orderBy, sortOrder)
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

    public override async Task<IEnumerable<RentalModel>> GetAsync(RentalSearchFilterModel filterModel)
    {
      var filters = GenerateFilterFuncs(filterModel);
      var orderBy = GenerateOrderByFunc(filterModel);
      return await GetAsync(filters, orderBy, filterModel.SortOrder, filterModel.Offset, filterModel.Limit);
    }

    private FilterFuncs GenerateFilterFuncs(RentalSearchFilterModel filterModel)
    {
      var filters = new FilterFuncs();
      filters.Add(r => r.RentalUnit.Bedrooms.Sum(b => b.BedCount) >= filterModel.BedsAtLeast);
      filters.Add(r => r.RentalUnit.Bedrooms.Count() >= filterModel.BedRoomsAtLeast);
      filters.Add(r => r.RentalUnit.Bathrooms.Count() >= filterModel.BathsAtLeast);

      if (!String.IsNullOrEmpty(filterModel.HasBedType))
      {
        filters.Add(r => r.RentalUnit.Bedrooms.Where(
                      b => b.BedType == filterModel.HasBedType).FirstOrDefault() != null);
      }

      if (!String.IsNullOrEmpty(filterModel.HasAmenity))
      {
        filters.Add(r => r.RentalUnit.Bedrooms.Where(
                      b => b.Amenities.Where(
                        a => a.Amenity == filterModel.HasAmenity)
                        .FirstOrDefault() != null)
                      .FirstOrDefault() != null);
      }

      return filters;
    }

    private OrderByFunc GenerateOrderByFunc(RentalSearchFilterModel filterModel)
    {
      if (!String.IsNullOrEmpty(filterModel.SortKey))
      {
        switch (filterModel.SortKey)
        {
          case "Id": return (e => e.Id);
          case "Name": return (e => e.Name);
          case "Description": return (e => e.Description);

          case "Beds": return (u => u.RentalUnit.Bedrooms.Sum(b => b.BedCount));
          case "Bedrooms": return (u => u.RentalUnit.Bedrooms.Count());
          case "Bathrooms": return (u => u.RentalUnit.Bathrooms.Count());

          case "Occupancy": return (u => u.RentalUnit.Occupancy);
        }
      }
      return null;
    }

  }
}
