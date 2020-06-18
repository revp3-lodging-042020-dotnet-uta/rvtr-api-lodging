using System.Threading.Tasks;

namespace RVTR.Lodging.DataContext.Repositories
{
  /// <summary>
  /// Represents the _UnitOfWork_ repository
  /// </summary>
  public class UnitOfWork
  {
    private readonly LodgingContext _context;

    public virtual LodgingRepository Lodging { get; }
    public virtual RentalRepository Rental { get; set; }
    public virtual ReviewRepository  Review { get; set; }

    public UnitOfWork(LodgingContext context)
    {
      _context = context;

      Lodging = new LodgingRepository(context);
      Rental = new RentalRepository(context);
      Review = new ReviewRepository(context);
    }

    /// <summary>
    /// Represents the _UnitOfWork_ `Commit` method
    /// </summary>
    /// <returns></returns>
    public async Task<int> CommitAsync() => await _context.SaveChangesAsync();
  }
}
