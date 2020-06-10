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

    public async Task DeleteAsync(int id)
    {
      throw new NotImplementedException();
    }

    public async Task<IEnumerable<RentalModel>> GetAsync()
    {
      throw new NotImplementedException();
    }

    public async Task GetAsync(int id)
    {
      throw new NotImplementedException();
    }

    public async Task InsertAsync(RentalModel entry)
    {
      throw new NotImplementedException();
    }

    public async Task Update(RentalModel entry)
    {
      throw new NotImplementedException();
    }
  }
}
