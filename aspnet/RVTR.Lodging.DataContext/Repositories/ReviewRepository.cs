using RVTR.Lodging.ObjectModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RVTR.Lodging.DataContext.Repositories
{
  public class ReviewRepository : IReviewRepository
  {
    public ReviewRepository(LodgingContext context)
    {

    }

    public async Task DeleteAsync(int id)
    {
      throw new NotImplementedException();
    }

    public async Task<IEnumerable<ReviewModel>> GetAsync()
    {
      throw new NotImplementedException();
    }

    public async Task GetAsync(int id)
    {
      throw new NotImplementedException();
    }

    public async Task InsertAsync(ReviewModel entry)
    {
      throw new NotImplementedException();
    }

    public async Task Update(ReviewModel entry)
    {
      throw new NotImplementedException();
    }
  }
}
