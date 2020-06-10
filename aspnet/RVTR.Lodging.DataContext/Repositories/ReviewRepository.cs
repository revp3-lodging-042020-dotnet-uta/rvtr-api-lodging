using RVTR.Lodging.ObjectModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RVTR.Lodging.DataContext.Repositories
{
  /// <summary>
  /// Implements the Review repository functions of the respective interface
  /// </summary>
  public class ReviewRepository : IReviewRepository
  {
    public readonly LodgingContext _db;

    public ReviewRepository(LodgingContext context)
    {
      _db = context;
    }

    public async Task<bool> DeleteAsync(int id)
    {
      var review = await _db.Reviews.FindAsync(id);
      _db.Remove(review);
      return await _db.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<ReviewModel>> GetAsync()
    {
      return await _db.Reviews.ToListAsync();
    }

    public async Task<ReviewModel> GetAsync(int id)
    {
      return await _db.Reviews.FindAsync(id);
    }

    public async Task<ReviewModel> InsertAsync(ReviewModel entry)
    {
      await _db.AddAsync(entry);
      await _db.SaveChangesAsync();
      return entry;
    }

    public async Task<ReviewModel> Update(ReviewModel entry)
    {
      _db.Update(entry);
      await _db.SaveChangesAsync();
      return entry;
    }
  }
}
