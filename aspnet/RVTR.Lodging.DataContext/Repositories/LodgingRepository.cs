using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.DataContext.Repositories
{
  public class LodgingRepository : Repository<LodgingModel>
  {
    public LodgingRepository(LodgingContext context) : base(context) {}
  }
}