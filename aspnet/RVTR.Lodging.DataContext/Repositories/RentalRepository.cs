using RVTR.Lodging.ObjectModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RVTR.Lodging.DataContext.Repositories
{
  public class RentalRepository : IRentalRepository
  {

    public RentalRepository(LodgingContext context)
    {

    }

    public Task<bool> DeleteAsync(int id)
    {
      throw new NotImplementedException();
    }

    public async Task<IEnumerable<RentalModel>> GetAsync()
    {
      throw new NotImplementedException();
    }

    public Task<RentalModel> GetAsync(int id)
    {
      throw new NotImplementedException();
    }

    public Task<RentalModel> InsertAsync(RentalModel entry)
    {
      throw new NotImplementedException();
    }

    public Task<RentalModel> Update()
    {
      throw new NotImplementedException();
    }
  }
}
