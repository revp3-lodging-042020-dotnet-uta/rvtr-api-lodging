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

    public async Task DeleteAsync(int id)
    {
      throw new NotImplementedException();
    }

    public async Task<IEnumerable<LodgingModel>> GetAsync()
    {
      throw new NotImplementedException();
    }

    public async Task GetAsync(int id)
    {
      throw new NotImplementedException();
    }

    public async Task InsertAsync(LodgingModel entry)
    {
      throw new NotImplementedException();
    }

    public async Task Update(LodgingModel entry)
    {
      throw new NotImplementedException();
    }
  }
}
