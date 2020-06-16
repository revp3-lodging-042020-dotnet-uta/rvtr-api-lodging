using RVTR.Lodging.DataContext;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class RentalSearchFilterTest
  {
    [Fact]
    public void Test_BedsAtLeast_Clamp()
    {
      var filterModel = new RentalSearchFilterModel();
      filterModel.BedsAtLeast = -1;
      Assert.Equal(0, filterModel.BedsAtLeast);

      filterModel.BedsAtLeast = 999;
      Assert.Equal(999, filterModel.BedsAtLeast);
    }

    [Fact]
    public void Test_BathsAtLeast_Clamp()
    {
      var filterModel = new RentalSearchFilterModel();
      filterModel.BathsAtLeast = -1;
      Assert.Equal(0, filterModel.BathsAtLeast);

      filterModel.BathsAtLeast = 999;
      Assert.Equal(999, filterModel.BathsAtLeast);
    }

  }
}