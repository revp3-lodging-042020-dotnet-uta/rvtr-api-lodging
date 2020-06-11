using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.DataContext.Repositories
{
  public class RentalRepository : Repository<RentalModel>
  {
    public RentalRepository(LodgingContext context) : base(context) {}
  }
}
