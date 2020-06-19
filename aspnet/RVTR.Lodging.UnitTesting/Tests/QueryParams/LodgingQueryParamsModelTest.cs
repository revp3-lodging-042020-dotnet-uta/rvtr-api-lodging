using RVTR.Lodging.DataContext;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class LodgingQueryParamsModelTest
  {
    [Fact]
    public void Test_Rating_Clamp()
    {
      var queryParams = new LodgingQueryParamsModel();
      queryParams.RatingAtLeast = -1;
      Assert.Equal(0, queryParams.RatingAtLeast);

      queryParams.RatingAtLeast = 999;
      Assert.Equal(10, queryParams.RatingAtLeast);

      queryParams.RatingAtLeast = 3;
      Assert.Equal(3, queryParams.RatingAtLeast);
    }

    [Fact]
    public void Test_SearchRadius_Clamp()
    {
      var queryParams = new LodgingQueryParamsModel();
      queryParams.SearchRadius = -1;
      Assert.Equal(1, queryParams.SearchRadius);

      queryParams.SearchRadius = 999;
      Assert.Equal(999, queryParams.SearchRadius);
    }

    [Fact]
    public void Test_BedsAtLeast_Clamp()
    {
      var queryParams = new LodgingQueryParamsModel();
      queryParams.BedsAtLeast = -1;
      Assert.Equal(0, queryParams.BedsAtLeast);

      queryParams.BedsAtLeast = 999;
      Assert.Equal(999, queryParams.BedsAtLeast);
    }

    [Fact]
    public void Test_BathsAtLeast_Clamp()
    {
      var queryParams = new LodgingQueryParamsModel();
      queryParams.BathsAtLeast = -1;
      Assert.Equal(0, queryParams.BathsAtLeast);

      queryParams.BathsAtLeast = 999;
      Assert.Equal(999, queryParams.BathsAtLeast);
    }

  }
}
