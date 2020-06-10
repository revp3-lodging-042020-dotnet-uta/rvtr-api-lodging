using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.DataContext.Repositories
{
  public interface IReviewRepository
  {
    /// <summary>
    /// Represents the Review repository interface
    /// </summary>
    Task<bool> DeleteAsync(int id);
    Task<ReviewModel> InsertAsync(ReviewModel entry);
    Task<IEnumerable<ReviewModel>> GetAsync();
    Task<ReviewModel> GetAsync(int id);
    Task<ReviewModel> Update(ReviewModel entry);
  }
}
