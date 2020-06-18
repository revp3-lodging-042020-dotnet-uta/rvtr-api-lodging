using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.DataContext;
using RVTR.Lodging.DataContext.Repositories;
using Xunit;
using System.Threading.Tasks;

namespace RVTR.Lodging.UnitTesting.Tests
{
    public class LodgingRepoTest
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
    public async void Test_LodgingRepo_GetAsync()
    {
      var dbOptions = await NewDb();

      using (var ctx = new LodgingContext(dbOptions))
      {
          await ctx.Database.EnsureCreatedAsync();
          await ctx.SaveChangesAsync();

          // Add repo-specific setup here.
          await ctx.SaveChangesAsync();
      }

      using (var ctx = new LodgingContext(dbOptions))
      {
          var repo = new LodgingRepository(ctx);

          // Add repo-specific method calls here.
          var actual = await repo.GetAsync(new LodgingSearchFilterModel());

          // Add Asserts here.
          Assert.Empty(actual);
      }
    }

    [Fact]
    public async void Test_LodgingRepo_GetAsyncById()
    {
      var dbOptions = await NewDb();

      using (var ctx = new LodgingContext(dbOptions))
      {
        await ctx.Database.EnsureCreatedAsync();
        await ctx.SaveChangesAsync();

        // Add repo-specific setup here.
        await ctx.SaveChangesAsync();
      }

      using (var ctx = new LodgingContext(dbOptions))
      {
        var repo = new LodgingRepository(ctx);

        // Add repo-specific method calls here.
        var actual = await repo.GetAsync(1, new LodgingSearchFilterModel());

        // Add Asserts here.
        Assert.Null(actual);
      }
    }
  }
}
