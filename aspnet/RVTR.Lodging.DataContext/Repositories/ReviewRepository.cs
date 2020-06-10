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

    public Task DeleteAsync(int id)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<ReviewModel>> GetAsync()
    {
      throw new NotImplementedException();
    }

    public Task GetAsync(int id)
    {
      throw new NotImplementedException();
    }

    public Task InsertAsync(ReviewModel entry)
    {
      throw new NotImplementedException();
    }

    public void Update()
    {
      throw new NotImplementedException();
    }
  }
}
