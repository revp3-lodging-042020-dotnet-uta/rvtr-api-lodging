using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.DataContext.Repositories
{
  public class LodgingRepository : Repository<LodgingModel>
  {
    public LodgingRepository(LodgingContext context) : base(context) {}

    //public override async Task<IEnumerable<LodgingModel>> Find(Expression<Func<LodgingModel, bool>> predicate)
    //{
      //return  await  
    //}
  }
}
