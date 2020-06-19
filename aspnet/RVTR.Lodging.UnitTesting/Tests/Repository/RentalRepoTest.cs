using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.DataContext;
using RVTR.Lodging.DataContext.Repositories;
using Xunit;
using System.Threading.Tasks;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.UnitTesting.Tests
{
    public class RentalRepoTest
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
      var queryParams = new RentalQueryParamsModel();
      queryParams.HasBedType = "King";
      queryParams.HasAmenity = "Coffee";

      var funcs = RentalRepository.GenerateFilterFuncs(queryParams);

      // 3 filters are always added, +2 more based on params.
      Assert.Equal(5, funcs.Count);
    }

    [Theory]
    [InlineData("Id")]
    [InlineData("Name")]
    [InlineData("Description")]

    [InlineData("Beds")]
    [InlineData("Bedrooms")]
    [InlineData("Bathrooms")]

    [InlineData("Occupancy")]
    public void Test_GenerateOrderByFunc(string sortKey)
    {
      var queryParams = new RentalQueryParamsModel();
      queryParams.SortKey = sortKey;

      var orderByFunc = RentalRepository.GenerateOrderByFunc(queryParams);

      Assert.NotNull(orderByFunc);
    }

    [Fact]
    public void Test_GenerateOrderByFunc_InvalidKey()
    {
      var queryParams = new RentalQueryParamsModel();
      queryParams.SortKey = "missing";

      var orderByFunc = RentalRepository.GenerateOrderByFunc(queryParams);

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
    public async void Test_RentalRepo_GetAsync()
    {
      var dbOptions = await NewDb();

      using (var ctx = new LodgingContext(dbOptions))
      {
        await ctx.Database.EnsureCreatedAsync();
        await ctx.SaveChangesAsync();
      }

      using (var ctx = new LodgingContext(dbOptions))
      {
        var repo = new RentalRepository(ctx);

        var actual = await repo.GetAsync(new RentalQueryParamsModel());

        Assert.Empty(actual);
      }
    }

    [Fact]
    public async void Test_RentalRepo_GetAsyncById()
    {
      var dbOptions = await NewDb();

      using (var ctx = new LodgingContext(dbOptions))
      {
        await ctx.Database.EnsureCreatedAsync();
        await ctx.SaveChangesAsync();
      }

      using (var ctx = new LodgingContext(dbOptions))
      {
        var repo = new RentalRepository(ctx);

        var actual = await repo.GetAsync(1, new RentalQueryParamsModel());

        Assert.Null(actual);
      }
    }
  }
}
