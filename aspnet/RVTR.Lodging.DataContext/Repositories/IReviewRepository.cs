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
    Task DeleteAsync(int id);
    Task InsertAsync(ReviewModel entry);
    Task<IEnumerable<ReviewModel>> GetAsync();
    Task GetAsync(int id);
    Task Update(ReviewModel entry);
  }
}
