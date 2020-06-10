using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.ObjectModel.Models;


namespace RVTR.Lodging.DataContext.Repositories
{
  /// <summary>
  /// Represents the Rental repository interface
  /// </summary>
  public interface IRentalRepository
  {
    Task DeleteAsync(int id);
    Task InsertAsync(RentalModel entry);
    Task <IEnumerable<RentalModel>> GetAsync();
    Task GetAsync(int id);
    void Update();
  }
}
