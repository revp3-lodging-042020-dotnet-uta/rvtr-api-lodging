using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.DataContext.Repositories
{

  /// <summary>
  /// Represents the lodging context
  /// </summary>
  /// <typeparam name="TEntity"></typeparam>
  public interface ILodgingRepository
  {
    Task DeleteAsync(int id);
    Task InsertAsync(LodgingModel entry);
    Task<IEnumerable<LodgingModel>> GetAsync();
    Task GetAsync(int id);
    void Update();
  }
}
//  public class LodgingRepository : ILodgingRepository
//  {
//    public readonly LodgingContext _db;

//    public LodgingRepository(LodgingContext context)
//    {
//      _db = context;
//    }

//    public virtual async Task DeleteAsync(int id) => _db.Remove(await SelectAsync(id));

//    public virtual async Task InsertAsync(LodgingContext entry) => await _db.AddAsync(entry).ConfigureAwait(true);

//    public virtual async Task<IEnumerable<LodgingContext>> SelectAsync() => await _db.ToListAsync();

//    public virtual async Task<LodgingContext> SelectAsync(int id) => await _db.FindAsync(id).ConfigureAwait(true);

//    public virtual void Update(LodgingContext entry) => _db.Update(entry);

//    public void Update()
//    {
//      throw new NotImplementedException();
//    }

//    Task ILodgingRepository.SelectAsync()
//    {
//      throw new NotImplementedException();
//    }

//    Task ILodgingRepository.SelectAsync(int id)
//    {
//      throw new NotImplementedException();
//    }
//  }
//}


//namespace RVTR.Lodging.DataContext.Repositories
//{
//  /// <summary>
//  /// Represents the _Repository_ generic
//  /// </summary>
//  /// <typeparam name="TEntity"></typeparam>
//  public interface Repository<TEntity> where TEntity : class
//  {
//    public readonly DbSet<TEntity> _db;

//    public Repository(LodgingContext context)
//    {
//      _db = context.Set<TEntity>();
//    }

//    public virtual async Task DeleteAsync(int id) => _db.Remove(await SelectAsync(id));

//    public virtual async Task InsertAsync(TEntity entry) => await _db.AddAsync(entry).ConfigureAwait(true);

//    public virtual async Task<IEnumerable<TEntity>> SelectAsync() => await _db.ToListAsync();

//    public virtual async Task<TEntity> SelectAsync(int id) => await _db.FindAsync(id).ConfigureAwait(true);

//    public virtual void Update(TEntity entry) => _db.Update(entry);
//  }
//}
