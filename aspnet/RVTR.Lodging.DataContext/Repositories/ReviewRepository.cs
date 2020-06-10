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

    public Task<bool> DeleteAsync(int id)
    {
      throw new NotImplementedException();
    }

    public async Task<IEnumerable<ReviewModel>> GetAsync()
    {
      throw new NotImplementedException();
    }

    public Task<ReviewModel> GetAsync(int id)
    {
      throw new NotImplementedException();
    }

    public Task<ReviewModel> InsertAsync(ReviewModel entry)
    {
      throw new NotImplementedException();
    }

    public Task<ReviewModel> Update()
    {
      throw new NotImplementedException();
    }
  }
}
