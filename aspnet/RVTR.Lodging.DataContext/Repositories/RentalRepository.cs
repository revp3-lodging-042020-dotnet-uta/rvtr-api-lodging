using RVTR.Lodging.ObjectModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RVTR.Lodging.DataContext.Repositories
{
  /// <summary>
  /// Implements the Rental repository functions of the respective interface
  /// </summary>
  public class RentalRepository : IRentalRepository
  {
    public readonly LodgingContext _db;

    public RentalRepository(LodgingContext context)
    {
      _db = context;
    }

    public async Task<bool> DeleteAsync(int id)
    {
      var rental = await _db.Rentals.FindAsync(id);
      _db.Remove(rental);
      return await _db.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<RentalModel>> GetAsync()
    {
      return await _db.Rentals.ToListAsync();
    }

    public async Task<RentalModel> GetAsync(int id)
    {
      return await _db.Rentals.FindAsync(id);
    }

    public async Task<RentalModel> InsertAsync(RentalModel entry)
    {
      await _db.AddAsync(entry);
      await _db.SaveChangesAsync();
      return entry;
    }

    public async Task<RentalModel> Update(RentalModel entry)
    {
      _db.Update(entry);
      await _db.SaveChangesAsync();
      return entry;
    }
  }
}
