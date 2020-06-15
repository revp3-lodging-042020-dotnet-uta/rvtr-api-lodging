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

    [Fact]
    public async void Test_Seed_HasLodgings()
    {
      var dbOptions = await NewDb();
      var lodging = new LodgingModel() { Id = 1 };

      using (var ctx = new LodgingContext(dbOptions))
      {
        await ctx.Database.EnsureCreatedAsync();
        await ctx.Lodgings.AddAsync(lodging);
        await ctx.SaveChangesAsync();

        // Setup for seeding the database
        Seed.SeedDatabase(ctx);
      }

      using (var ctx = new LodgingContext(dbOptions))
      {
        // Add Asserts here.
        Assert.True(ctx.Lodgings.Count() > 0);
        Assert.False(ctx.Rentals.Count() > 0);
        Assert.False(ctx.Reviews.Count() > 0);
      }
    }


    [Fact]
    public async void Test_Seed_HasRentals()
    {
      var dbOptions = await NewDb();
      var rental = new RentalModel() { Id = 1 };

      using (var ctx = new LodgingContext(dbOptions))
      {
        await ctx.Database.EnsureCreatedAsync();
        await ctx.Rentals.AddAsync(rental);
        await ctx.SaveChangesAsync();

        // Setup for seeding the database
        Seed.SeedDatabase(ctx);
      }

      using (var ctx = new LodgingContext(dbOptions))
      {
        // Add Asserts here.
        Assert.False(ctx.Lodgings.Count() > 0);
        Assert.True(ctx.Rentals.Count() > 0);
        Assert.False(ctx.Reviews.Count() > 0);
      }
    }

    [Fact]
    public async void Test_Seed_HasReviews()
    {
      var dbOptions = await NewDb();
      var review = new ReviewModel() { Id = 1 };

      using (var ctx = new LodgingContext(dbOptions))
      {
        await ctx.Database.EnsureCreatedAsync();
        await ctx.Reviews.AddAsync(review);
        await ctx.SaveChangesAsync();

        // Setup for seeding the database
        Seed.SeedDatabase(ctx);
      }

      using (var ctx = new LodgingContext(dbOptions))
      {
        // Add Asserts here.
        Assert.False(ctx.Lodgings.Count() > 0);
        Assert.False(ctx.Rentals.Count() > 0);
        Assert.True(ctx.Reviews.Count() > 0);
      }
    }
  }
}
