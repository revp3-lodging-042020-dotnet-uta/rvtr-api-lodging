using RVTR.Lodging.DataContext;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class ReviewSearchFilterTest
  {
    [Fact]
    public void Test_Rating_Clamp()
    {
      var filterModel = new ReviewSearchFilterModel();
      filterModel.RatingAtLeast = 0;
      Assert.Equal(1, filterModel.RatingAtLeast);

      filterModel.RatingAtLeast = 999;
      Assert.Equal(5, filterModel.RatingAtLeast);

      filterModel.RatingAtLeast = 3;
      Assert.Equal(3, filterModel.RatingAtLeast);
    }

    [Fact]
    public void Test_LodgingId()
    {
      var filterModel = new ReviewSearchFilterModel();
      Assert.Null(filterModel.LodgingId);

      filterModel.LodgingId = 0;
      Assert.Equal(0, filterModel.LodgingId);
    }

    [Fact]
    public void Test_AccountId()
    {
      var filterModel = new ReviewSearchFilterModel();
      Assert.Null(filterModel.AccountId);

      filterModel.AccountId = 0;
      Assert.Equal(0, filterModel.AccountId);
    }
  }
}