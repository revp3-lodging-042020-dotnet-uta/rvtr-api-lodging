using RVTR.Lodging.DataContext;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class LodgingSearchFilterModelTest
  {
    [Fact]
    public void Test_Rating_Clamp()
    {
      var filterModel = new LodgingSearchFilterModel();
      filterModel.RatingAtLeast = -1;
      Assert.Equal(0, filterModel.RatingAtLeast);

      filterModel.RatingAtLeast = 999;
      Assert.Equal(10, filterModel.RatingAtLeast);

      filterModel.RatingAtLeast = 3;
      Assert.Equal(3, filterModel.RatingAtLeast);
    }

    [Fact]
    public void Test_SearchRadius_Clamp()
    {
      var filterModel = new LodgingSearchFilterModel();
      filterModel.SearchRadius = -1;
      Assert.Equal(1, filterModel.SearchRadius);

      filterModel.SearchRadius = 999;
      Assert.Equal(999, filterModel.SearchRadius);
    }

    [Fact]
    public void Test_BedsAtLeast_Clamp()
    {
      var filterModel = new LodgingSearchFilterModel();
      filterModel.BedsAtLeast = -1;
      Assert.Equal(0, filterModel.BedsAtLeast);

      filterModel.BedsAtLeast = 999;
      Assert.Equal(999, filterModel.BedsAtLeast);
    }

    [Fact]
    public void Test_BathsAtLeast_Clamp()
    {
      var filterModel = new LodgingSearchFilterModel();
      filterModel.BathsAtLeast = -1;
      Assert.Equal(0, filterModel.BathsAtLeast);

      filterModel.BathsAtLeast = 999;
      Assert.Equal(999, filterModel.BathsAtLeast);
    }

  }
}
