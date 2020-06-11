using RVTR.Lodging.ObjectModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RVTR.Lodging.DataContext.Repositories
{
  public class ReviewRepository : Repository<ReviewModel>
  {
    public ReviewRepository(LodgingContext context) : base(context) {}
  }
}