using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.DataContext;
using RVTR.Lodging.DataContext.Repositories;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class UnitOfWorkTest
  {
    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
    private static readonly DbContextOptions<LodgingContext> _options = new DbContextOptionsBuilder<LodgingContext>().UseSqlite(_connection).Options;

    [Fact]
    public async void Test_UnitOfWork_CommitAsync()
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new LodgingContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
        }

        using (var ctx = new LodgingContext(_options))
        {
          var unitOfWork = new UnitOfWork(ctx);
          var actual = await unitOfWork.CommitAsync();

          Assert.NotNull(unitOfWork.Lodging);
          Assert.NotNull(unitOfWork.Rental);
          Assert.NotNull(unitOfWork.Review);
          Assert.Equal(0, actual);
        }
      }
      finally
      {
        await _connection.CloseAsync();
      }
    }
  }
}
