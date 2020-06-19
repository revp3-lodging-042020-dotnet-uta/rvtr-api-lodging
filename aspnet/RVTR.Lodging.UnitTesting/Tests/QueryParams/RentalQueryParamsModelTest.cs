using RVTR.Lodging.DataContext;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class RentalQueryParamsModelTest
  {
    [Fact]
    public void Test_BedsAtLeast_Clamp()
    {
      var queryParams = new RentalQueryParamsModel();
      queryParams.BedsAtLeast = -1;
      Assert.Equal(0, queryParams.BedsAtLeast);

      queryParams.BedsAtLeast = 999;
      Assert.Equal(999, queryParams.BedsAtLeast);
    }

    [Fact]
    public void Test_BathsAtLeast_Clamp()
    {
      var queryParams = new RentalQueryParamsModel();
      queryParams.BathsAtLeast = -1;
      Assert.Equal(0, queryParams.BathsAtLeast);

      queryParams.BathsAtLeast = 999;
      Assert.Equal(999, queryParams.BathsAtLeast);
    }

  }
}