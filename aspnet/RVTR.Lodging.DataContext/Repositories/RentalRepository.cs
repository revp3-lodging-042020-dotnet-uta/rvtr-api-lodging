using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.DataContext.Repositories
{

  /// <summary>
  /// Function to be applied for results filtering.
  /// </summary>
  using FilterFuncs = List<Expression<Func<RentalModel, bool>>>;
  /// <summary>
  /// Function to be applied for result ordering.
  /// </summary>
  using OrderByFunc = Expression<Func<RentalModel, Object>>;

  public class RentalRepository : Repository<RentalModel, RentalSearchFilterModel>
  {
    private LodgingContext dbContext;


    public RentalRepository(LodgingContext context) : base(context)
    {
      this.dbContext = context;
    }

    /// <summary>
    /// EFCore "Include" functions for including additional data in the query.
    /// </summary>
    /// <param name="filterModel"></param>
    /// <returns></returns>
    private IQueryable<RentalModel> IncludeQuery(RentalSearchFilterModel filterModel)
    {
      return dbContext.Rentals
        .Include(x => x.Lodging)
        .Include(x => x.RentalUnit).ThenInclude(x => x.Bathrooms)
        .Include(x => x.RentalUnit).ThenInclude(x => x.Bedrooms).ThenInclude(x => x.Images)
        .Include(x => x.RentalUnit).ThenInclude(x => x.Bedrooms).ThenInclude(x => x.Amenities);
    }

    /// <summary>
    /// Executes a database query for a specific entity ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="filterModel"></param>
    /// <returns></returns>
    public override async Task<RentalModel> GetAsync(int id, RentalSearchFilterModel filterModel)
    {
      return await IncludeQuery(filterModel)
        .AsNoTracking()
        .Where(e => e.Id == id)
        .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Configures an executes a database query based on filtering parameters.
    /// </summary>
    /// <param name="filterModel"></param>
    /// <returns></returns>
    public override async Task<IEnumerable<RentalModel>> GetAsync(RentalSearchFilterModel filterModel)
    {
      var filters = GenerateFilterFuncs(filterModel);
      var orderBy = GenerateOrderByFunc(filterModel);
      var query = IncludeQuery(filterModel);
      return await GetAsync(query, filters, orderBy, filterModel.SortOrder, filterModel.Offset, filterModel.Limit);
    }

    /// <summary>
    /// Generates filtering functions based on user-supplied filtering parameters.
    /// </summary>
    /// <param name="filterModel"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Generates ordering functions based on user-supplied data.
    /// </summary>
    /// <param name="filterModel"></param>
    /// <returns></returns>
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
