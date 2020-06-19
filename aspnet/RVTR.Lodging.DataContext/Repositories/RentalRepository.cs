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

  public class RentalRepository : Repository<RentalModel, RentalQueryParamsModel>
  {
    private readonly LodgingContext dbContext;

    public RentalRepository(LodgingContext context) : base(context)
    {
      this.dbContext = context;
    }

    /// <summary>
    /// EFCore "Include" functions for including additional data in the query.
    /// </summary>
    /// <param name="queryParams"></param>
    /// <returns></returns>
    private IQueryable<RentalModel> IncludeQuery()
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
    /// <param name="queryParams"></param>
    /// <returns></returns>
    public override async Task<RentalModel> GetAsync(int id, RentalQueryParamsModel queryParams)
    {
      return await IncludeQuery()
        .AsNoTracking()
        .Where(e => e.Id == id)
        .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Configures and executes a database query based on query parameters.
    /// </summary>
    /// <param name="queryParams"></param>
    /// <returns></returns>
    public override async Task<IEnumerable<RentalModel>> GetAsync(RentalQueryParamsModel queryParams)
    {
      var filters = GenerateFilterFuncs(queryParams);
      var orderBy = GenerateOrderByFunc(queryParams);
      var query = IncludeQuery();
      return await GetAsync(query, filters, orderBy, queryParams.SortOrder, queryParams.Offset, queryParams.Limit);
    }

    /// <summary>
    /// Generates filtering functions based on user-supplied query parameters.
    /// </summary>
    /// <param name="queryParams"></param>
    /// <returns></returns>
    private FilterFuncs GenerateFilterFuncs(RentalQueryParamsModel queryParams)
    {
      // The funcs created here simply return true if there is an element matching
      // the filter parameter, or false if the element does not match. We use a
      // FirstOrDefault overload that acts as a Where clause. If there is a match,
      // then the item is valid for our filtering and should be included in the
      // result set. If not, then the item should not be included in the result set.

      var filters = new FilterFuncs();
      filters.Add(r => r.RentalUnit.Bedrooms.Sum(b => b.BedCount) >= queryParams.BedsAtLeast);
      filters.Add(r => r.RentalUnit.Bedrooms.Count() >= queryParams.BedRoomsAtLeast);
      filters.Add(r => r.RentalUnit.Bathrooms.Count() >= queryParams.BathsAtLeast);

      if (!String.IsNullOrEmpty(queryParams.HasBedType))
      {
        filters.Add(r => r.RentalUnit.Bedrooms.FirstOrDefault(
                      b => b.BedType == queryParams.HasBedType) != null);
      }

      if (!String.IsNullOrEmpty(queryParams.HasAmenity))
      {
        filters.Add(r => r.RentalUnit.Bedrooms.FirstOrDefault(
                      b => b.Amenities.FirstOrDefault(
                        a => a.Amenity == queryParams.HasAmenity) != null) != null);
      }

      return filters;
    }

    /// <summary>
    /// Generates ordering functions based on user-supplied data.
    /// </summary>
    /// <param name="queryParams"></param>
    /// <returns></returns>
    private OrderByFunc GenerateOrderByFunc(RentalQueryParamsModel queryParams)
    {
      if (!String.IsNullOrEmpty(queryParams.SortKey))
      {
        switch (queryParams.SortKey)
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
