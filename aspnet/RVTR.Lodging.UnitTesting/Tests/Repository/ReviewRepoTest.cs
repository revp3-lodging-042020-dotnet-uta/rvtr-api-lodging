using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.DataContext;
using RVTR.Lodging.DataContext.Repositories;
using Xunit;
using System.Threading.Tasks;

namespace RVTR.Lodging.UnitTesting.Tests
{
    public class ReviewRepoTest
  {

    private async Task<DbContextOptions<LodgingContext>> NewDb()
    {
      var connection = new SqliteConnection("Data Source=:memory:");
      await connection.OpenAsync();
      return new DbContextOptionsBuilder<LodgingContext>()
          .UseSqlite(connection)
          .Options;
    }

    [Fact]
    public void Test_GenerateFilterFuncs()
    {
      var queryParams = new ReviewQueryParamsModel();
      queryParams.AccountId = 1;
      queryParams.LodgingId = 1;

      var funcs = ReviewRepository.GenerateFilterFuncs(queryParams);

      // 1 filter is always added, +2 more based on params.
      Assert.Equal(3, funcs.Count);
    }

    [Theory]
    [InlineData("Id")]
    [InlineData("AccountId")]
    [InlineData("Comment")]
    [InlineData("DateCreated")]
    [InlineData("Rating")]
    public void Test_GenerateOrderByFunc(string sortKey)
    {
      var queryParams = new ReviewQueryParamsModel();
      queryParams.SortKey = sortKey;

      var orderByFunc = ReviewRepository.GenerateOrderByFunc(queryParams);

      Assert.NotNull(orderByFunc);
    }

    [Fact]
    public void Test_GenerateOrderByFunc_InvalidKey()
    {
      var queryParams = new ReviewQueryParamsModel();
      queryParams.SortKey = "missing";

      var orderByFunc = ReviewRepository.GenerateOrderByFunc(queryParams);

      Assert.Null(orderByFunc);
    }

    [Fact]
    public void Test_GenerateOrderByFunc_EmptyKey()
    {
      var queryParams = new RentalQueryParamsModel();

      var orderByFunc = RentalRepository.GenerateOrderByFunc(queryParams);

      Assert.Null(orderByFunc);
    }

    [Fact]
    public async void Test_ReviewRepo_GetAsync()
    {
      var dbOptions = await NewDb();

      using (var ctx = new LodgingContext(dbOptions))
      {
        await ctx.Database.EnsureCreatedAsync();
        await ctx.SaveChangesAsync();
      }

      using (var ctx = new LodgingContext(dbOptions))
      {
        var repo = new ReviewRepository(ctx);

        var actual = await repo.GetAsync(new ReviewQueryParamsModel());

        Assert.Empty(actual);
      }
    }

    [Fact]
    public async void Test_ReviewRepo_GetAsyncById()
    {
      var dbOptions = await NewDb();

      using (var ctx = new LodgingContext(dbOptions))
      {
        await ctx.Database.EnsureCreatedAsync();
        await ctx.SaveChangesAsync();
      }

      using (var ctx = new LodgingContext(dbOptions))
      {
        var repo = new ReviewRepository(ctx);

        var actual = await repo.GetAsync(1, new ReviewQueryParamsModel());

        Assert.Null(actual);
      }
    }
  }
}
