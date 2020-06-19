using RVTR.Lodging.DataContext;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class ReviewQueryParamsModelTest
  {
    [Theory]
    [InlineData("RatingAtLeast", 0, 10)]
    public void Test_Clamped_Props_Double(string propName, double min, double max)
    {
      var queryParams = new ReviewQueryParamsModel();
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
    [InlineData("LodgingId")]
    [InlineData("AccountId")]
    public void Test_Trivial_NullableInt_Props(string propName)
    {
      var queryParams = new ReviewQueryParamsModel();
      var prop = queryParams.GetType().GetProperty(propName);
      prop.SetValue(queryParams, 1);
      Assert.Equal(1, prop.GetValue(queryParams));
    }

  }
}