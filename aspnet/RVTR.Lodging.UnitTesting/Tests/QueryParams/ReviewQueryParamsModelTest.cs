using RVTR.Lodging.DataContext;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class ReviewQueryParamsModelTest
  {
    [Fact]
    public void Test_Rating_Clamp()
    {
      var queryParams = new ReviewQueryParamsModel();
      queryParams.RatingAtLeast = -1;
      Assert.Equal(0, queryParams.RatingAtLeast);

      queryParams.RatingAtLeast = 999;
      Assert.Equal(10, queryParams.RatingAtLeast);

      queryParams.RatingAtLeast = 3;
      Assert.Equal(3, queryParams.RatingAtLeast);
    }

    [Fact]
    public void Test_LodgingId()
    {
      var queryParams = new ReviewQueryParamsModel();
      Assert.Null(queryParams.LodgingId);

      queryParams.LodgingId = 0;
      Assert.Equal(0, queryParams.LodgingId);
    }

    [Fact]
    public void Test_AccountId()
    {
      var queryParams = new ReviewQueryParamsModel();
      Assert.Null(queryParams.AccountId);

      queryParams.AccountId = 0;
      Assert.Equal(0, queryParams.AccountId);
    }
  }
}