using RVTR.Lodging.ObjectModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RVTR.Lodging.DataContext.Repositories
{
  /// <summary>
  /// Implements the lodging repository functions of the respective interface
  /// </summary>
  public class LodgingRepository : ILodgingRepository
  {
    public readonly LodgingContext _db;

    public LodgingRepository(LodgingContext context)
    {
      _db = context;
    }

    public async Task<bool> DeleteAsync(int id)
    {
      var lodge = await _db.Lodgings.FindAsync(id);
      _db.Remove(lodge);
      return await _db.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<LodgingModel>> GetAsync()
    {
      return await _db.Lodgings.ToListAsync();
    }

    public async Task<LodgingModel> GetAsync(int id)
    {
      return await _db.Lodgings.FindAsync(id);
    }

    public async Task<LodgingModel> InsertAsync(LodgingModel entry)
    {
      await _db.AddAsync(entry);
      await _db.SaveChangesAsync();
      return entry;
    }

    public async Task<LodgingModel> Update(LodgingModel entry)
    {
      _db.Update(entry);
      await _db.SaveChangesAsync();
      return entry;
    }
  }
}
