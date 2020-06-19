using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.DataContext;
using RVTR.Lodging.DataContext.Repositories;
using Xunit;
using System.Threading.Tasks;
using RVTR.Lodging.ObjectModel.Models;

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

    [Fact]
    public void Test_GenerateFilterFuncs()
    {
      var queryParams = new LodgingQueryParamsModel();
      queryParams.HasBedType = "King";
      queryParams.HasAmenity = "Coffee";
      queryParams.City = "test";

      var funcs = LodgingRepository.GenerateFilterFuncs(queryParams);

      // 4 filters are always added, +3 more based on params.
      Assert.Equal(7, funcs.Count);
    }

    [Theory]
    [InlineData("Id")]
    [InlineData("Name")]
    [InlineData("Description")]

    [InlineData("Location.Id")]
    [InlineData("Location.Latitude")]
    [InlineData("Location.Longitude")]
    [InlineData("Location.Locale")]

    [InlineData("Location.Address.Id")]
    [InlineData("Location.Address.City")]
    [InlineData("Location.Address.Country")]
    [InlineData("Location.Address.PostalCode")]
    [InlineData("Location.Address.StateProvince")]
    [InlineData("Location.Address.Street")]

    [InlineData("Rentals")]
    [InlineData("Beds")]
    [InlineData("Bedrooms")]
    [InlineData("Bathrooms")]
    [InlineData("Occupancy")]

    [InlineData("ReviewCount")]
    [InlineData("ReviewAverageRating")]
    public void Test_GenerateOrderByFunc(string sortKey)
    {
      var queryParams = new LodgingQueryParamsModel();
      queryParams.SortKey = sortKey;

      var orderByFunc = LodgingRepository.GenerateOrderByFunc(queryParams);

      Assert.NotNull(orderByFunc);
    }

    [Fact]
    public void Test_GenerateOrderByFunc_InvalidKey()
    {
      var queryParams = new LodgingQueryParamsModel();
      queryParams.SortKey = "missing";

      var orderByFunc = LodgingRepository.GenerateOrderByFunc(queryParams);

      Assert.Null(orderByFunc);
    }

    [Fact]
    public void Test_GenerateOrderByFunc_EmptyKey()
    {
      var queryParams = new LodgingQueryParamsModel();

      var orderByFunc = LodgingRepository.GenerateOrderByFunc(queryParams);

      Assert.Null(orderByFunc);
    }

    [Fact]
    public async void Test_LodgingRepo_GetAsync_IncludeImages()
    {
      var dbOptions = await NewDb();

      using (var ctx = new LodgingContext(dbOptions))
      {
          await ctx.Database.EnsureCreatedAsync();
          await ctx.SaveChangesAsync();
      }

      using (var ctx = new LodgingContext(dbOptions))
      {
          var repo = new LodgingRepository(ctx);

          var queryParams = new LodgingQueryParamsModel();
          queryParams.IncludeImages = true;

          var actual = await repo.GetAsync(queryParams);

          Assert.Empty(actual);
      }
    }

    [Fact]
    public async void Test_LodgingRepo_GetAsync()
    {
      var dbOptions = await NewDb();

      using (var ctx = new LodgingContext(dbOptions))
      {
          await ctx.Database.EnsureCreatedAsync();
          await ctx.SaveChangesAsync();
      }

      using (var ctx = new LodgingContext(dbOptions))
      {
          var repo = new LodgingRepository(ctx);

          var actual = await repo.GetAsync(new LodgingQueryParamsModel());

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
      }

      using (var ctx = new LodgingContext(dbOptions))
      {
        var repo = new LodgingRepository(ctx);

        var actual = await repo.GetAsync(1, new LodgingQueryParamsModel());

        Assert.Null(actual);
      }
    }
  }
}
