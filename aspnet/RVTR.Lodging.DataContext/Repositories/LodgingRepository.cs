using RVTR.Lodging.ObjectModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RVTR.Lodging.DataContext.Repositories
{
  public class LodgingRepository : ILodgingRepository
  {
    public LodgingRepository(LodgingContext context)
    {

    }

    public Task<bool> DeleteAsync(int id)
    {
      throw new NotImplementedException();
    }

    public async Task<IEnumerable<LodgingModel>> GetAsync()
    {
      throw new NotImplementedException();
    }

    public Task<LodgingModel> GetAsync(int id)
    {
      throw new NotImplementedException();
    }

    public Task<LodgingModel> InsertAsync(LodgingModel entry)
    {
      throw new NotImplementedException();
    }

    public Task<LodgingModel> Update()
    {
      throw new NotImplementedException();
    }
  }
}
