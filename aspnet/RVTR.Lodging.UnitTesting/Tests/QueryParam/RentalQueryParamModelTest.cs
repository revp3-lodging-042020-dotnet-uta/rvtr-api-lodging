using RVTR.Lodging.DataContext;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class RentalQueryParamModelTest
  {
    [Fact]
    public void Test_BedsAtLeast_Clamp()
    {
      var queryParams = new RentalQueryParamModel();
      queryParams.BedsAtLeast = -1;
      Assert.Equal(0, queryParams.BedsAtLeast);

      queryParams.BedsAtLeast = 999;
      Assert.Equal(999, queryParams.BedsAtLeast);
    }

    [Fact]
    public void Test_BathsAtLeast_Clamp()
    {
      var queryParams = new RentalQueryParamModel();
      queryParams.BathsAtLeast = -1;
      Assert.Equal(0, queryParams.BathsAtLeast);

      queryParams.BathsAtLeast = 999;
      Assert.Equal(999, queryParams.BathsAtLeast);
    }

  }
}