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
  using FilterFuncs = List<Expression<Func<LodgingModel, bool>>>;
  /// <summary>
  /// Function to be applied for result ordering.
  /// </summary>
  using OrderByFunc = Expression<Func<LodgingModel, Object>>;

  public class LodgingRepository : Repository<LodgingModel, LodgingSearchFilterModel>
  {
    private LodgingContext dbContext;

    public LodgingRepository(LodgingContext context) : base(context)
    {
      this.dbContext = context;
    }

    /// <summary>
    /// EFCore "Include" functions for including additional data in the query.
    /// </summary>
    /// <param name="filterModel"></param>
    /// <returns></returns>
    private IQueryable<LodgingModel> IncludeQuery(LodgingSearchFilterModel filterModel)
    {
      var query = dbContext.Lodgings.AsQueryable();

      if (filterModel != null) {
        if (filterModel.IncludeImages == true) {
          query = query
            .Include(x => x.Images)
            .Include(x => x.Rentals).ThenInclude(x => x.RentalUnit).ThenInclude(x => x.Bedrooms).ThenInclude(x => x.Images);
        }
      }

      return query
        .Include(x => x.Location).ThenInclude(x => x.Address)
        .Include(x => x.Amenities)
        .Include(x => x.Rentals).ThenInclude(x => x.RentalUnit).ThenInclude(x => x.Bathrooms)
        .Include(x => x.Rentals).ThenInclude(x => x.RentalUnit).ThenInclude(x => x.Bedrooms).ThenInclude(x => x.Amenities)
        .Include(x => x.Reviews);
    }

    /// <summary>
    /// Executes a database query for all entities based on filtering parameters.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="filters"></param>
    /// <param name="orderBy"></param>
    /// <param name="sortOrder"></param>
    /// <param name="resultOffset">start results from this value</param>
    /// <param name="maxResults"></param>
    /// <returns></returns>
    protected override async Task<IEnumerable<LodgingModel>> GetAsync(
                                                          IQueryable<LodgingModel> query = null,
                                                          FilterFuncs filters = null,
                                                          OrderByFunc orderBy = null,
                                                          string sortOrder = "asc",
                                                          int resultOffset = 0,
                                                          int maxResults = 50)
    {
      return await this.Select(query, filters, orderBy, sortOrder)
        .AsNoTracking()
        .Skip(resultOffset)
        .Take(maxResults)
        .ToListAsync();
    }

    /// <summary>
    /// Executes a database query for a specific entity ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="filterModel"></param>
    /// <returns></returns>
    public override async Task<LodgingModel> GetAsync(int id, LodgingSearchFilterModel filterModel)
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
    public override async Task<IEnumerable<LodgingModel>> GetAsync(LodgingSearchFilterModel filterModel)
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
    private FilterFuncs GenerateFilterFuncs(LodgingSearchFilterModel filterModel)
    {
      var filters = new FilterFuncs();
      filters.Add(m => m.Reviews.Average(r => r.Rating) >= filterModel.RatingAtLeast);
      filters.Add(m => m.Rentals.Where(r => r.RentalUnit.Bedrooms.Count() >= filterModel.BedRoomsAtLeast).FirstOrDefault() != null);
      filters.Add(m => m.Rentals.Where(r => r.RentalUnit.Bathrooms.Count() >= filterModel.BathsAtLeast).FirstOrDefault() != null);

      filters.Add(m => m.Rentals.Where(
                    r => r.RentalUnit.Bedrooms.Where(
                      b => b.BedCount >= filterModel.BedsAtLeast)
                      .FirstOrDefault() != null)
                    .FirstOrDefault() != null);

      if (!String.IsNullOrEmpty(filterModel.HasBedType))
      {
        filters.Add(m => m.Rentals.Where(
                      r => r.RentalUnit.Bedrooms.Where(
                        b => b.BedType == filterModel.HasBedType)
                        .FirstOrDefault() != null)
                      .FirstOrDefault() != null);
      }

      if (!String.IsNullOrEmpty(filterModel.HasAmenity))
      {
        filters.Add(m => m.Amenities.Where(a => a.Amenity == filterModel.HasAmenity).FirstOrDefault() != null);
      }

      if (!String.IsNullOrEmpty(filterModel.City))
      {
        filters.Add(m => m.Location.Address.City.ToLower().Contains(filterModel.City.ToLower()));
      }

      return filters;
    }


    /// <summary>
    /// Generates ordering functions based on user-supplied data.
    /// </summary>
    /// <param name="filterModel"></param>
    /// <returns></returns>
    private OrderByFunc GenerateOrderByFunc(LodgingSearchFilterModel filterModel)
    {
      if (!String.IsNullOrEmpty(filterModel.SortKey))
      {
        switch (filterModel.SortKey)
        {
          case "Id": return (e => e.Id);
          case "Name": return (e => e.Name);
          case "Description": return (e => e.Description);

          case "Location.Id": return (e => e.Location.Id);
          case "Location.Latitude": return (e => e.Location.Latitude);
          case "Location.Longitude": return (e => e.Location.Longitude);
          case "Location.Locale": return (e => e.Location.Locale);

          case "Location.Address.Id": return (e => e.Location.Address.Id);
          case "Location.Address.City": return (e => e.Location.Address.City);
          case "Location.Address.Country": return (e => e.Location.Address.Country);
          case "Location.Address.PostalCode": return (e => e.Location.Address.PostalCode);
          case "Location.Address.StateProvince": return (e => e.Location.Address.StateProvince);
          case "Location.Address.Street": return (e => e.Location.Address.Street);

          case "Rentals": return (e => e.Rentals.Count());
          case "Beds": return (e => e.Rentals.Sum(u => u.RentalUnit.Bedrooms.Sum(b => b.BedCount)));
          case "Bedrooms": return (e => e.Rentals.Sum(u => u.RentalUnit.Bedrooms.Count()));
          case "Bathrooms": return (e => e.Rentals.Sum(u => u.RentalUnit.Bathrooms.Count()));
          case "Occupancy": return (e => e.Rentals.Sum(u => u.RentalUnit.Occupancy));

          case "ReviewCount": return (e => e.Reviews.Count());
          case "ReviewAverageRating": return (e => e.Reviews.Average(r => r.Rating));
        }
      }
      return null;
    }

  }
}
