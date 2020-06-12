using System.Linq;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.DataContext;
using RVTR.Lodging.DataContext.Repositories;
using RVTR.Lodging.ObjectModel.Models;
using Xunit;
using System.Threading.Tasks;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class SeedTest
  {

    private async Task<DbContextOptions<LodgingContext>> NewDb()
    {
      var connection = new SqliteConnection("Data Source=:memory:");
      await connection.OpenAsync();
      return new DbContextOptionsBuilder<LodgingContext>()
          .UseSqlite(connection)
          .Options;
    }

    // Sample test if there is LodgingRepo specific functionality to test.
    [Fact]
    public async void Test_Seed_Initializer()
    {
      var dbOptions = await NewDb();

      using (var ctx = new LodgingContext(dbOptions))
      {
        await ctx.Database.EnsureCreatedAsync();
        await ctx.SaveChangesAsync();

        // Setup for seeding the database
        Seed.SeedDatabase(ctx);
      }

      using (var ctx = new LodgingContext(dbOptions))
      {
        // Add Asserts here.
        Assert.True(ctx.Lodgings.Count() > 0);
        Assert.True(ctx.Rentals.Count() > 0);
        Assert.True(ctx.Reviews.Count() > 0);
      }
    }
  }
}
