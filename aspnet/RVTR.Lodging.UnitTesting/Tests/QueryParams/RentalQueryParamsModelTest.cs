using RVTR.Lodging.DataContext;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class RentalQueryParamsModelTest
  {
    [Theory]
    [InlineData("BedsAtLeast", 0, 0)]
    [InlineData("BathsAtLeast", 0, 0)]
    [InlineData("BedRoomsAtLeast", 0, 0)]
    public void Test_Clamped_Props_Int(string propName, int min, int max)
    {
      var queryParams = new RentalQueryParamsModel();
      var prop = queryParams.GetType().GetProperty(propName);

      prop.SetValue(queryParams, -99999999);
      Assert.Equal(min, prop.GetValue(queryParams));

      if (min != max)
      {
        prop.SetValue(queryParams, 99999999);
        Assert.Equal(max, prop.GetValue(queryParams));
      }
    }

    [Theory]
    [InlineData("HasBedType")]
    [InlineData("HasAmenity")]
    public void Test_Trivial_String_Props(string propName)
    {
      var queryParams = new RentalQueryParamsModel();
      var prop = queryParams.GetType().GetProperty(propName);
      prop.SetValue(queryParams, "test");
      Assert.Equal("test", prop.GetValue(queryParams));
    }
  }
}