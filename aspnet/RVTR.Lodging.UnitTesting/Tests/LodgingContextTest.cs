using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.DataContext;
using RVTR.Lodging.ObjectModel.Models;
using Xunit;
using System.Threading.Tasks;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class LodgingContextTest
  {
      [Fact]
      public void InstantiateLodgingContext()
      {
          var _ = new LodgingContext();
      }
  }
}
