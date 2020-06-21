using RVTR.Lodging.DataContext;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class LodgingQueryParamsModelTest
  {
    [Theory]
    [InlineData("BedsAtLeast", 0, 0)]
    [InlineData("BathsAtLeast", 0, 0)]
    [InlineData("BedRoomsAtLeast", 0, 0)]
    public void Test_Clamped_Props_Int(string propName, int min, int max)
    {
      var queryParams = new LodgingQueryParamsModel();
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
    [InlineData("RatingAtLeast", 0, 10)]
    public void Test_Clamped_Props_Double(string propName, double min, double max)
    {
      var queryParams = new LodgingQueryParamsModel();
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
    [InlineData("City")]
    [InlineData("Name")]
    [InlineData("HasAmenity")]
    [InlineData("HasBedType")]
    public void Test_Trivial_String_Props(string propName)
    {
      var queryParams = new LodgingQueryParamsModel();
      var prop = queryParams.GetType().GetProperty(propName);
      prop.SetValue(queryParams, "test");
      Assert.Equal("test", prop.GetValue(queryParams));
    }

    [Theory]
    [InlineData("IncludeImages")]
    public void Test_Trivial_Bool_Props(string propName)
    {
      var queryParams = new LodgingQueryParamsModel();
      var prop = queryParams.GetType().GetProperty(propName);
      prop.SetValue(queryParams, true);
      Assert.Equal(true, prop.GetValue(queryParams));
    }

  }
}
