using RVTR.Lodging.DataContext;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class QueryParamsModelTest
  {

    [Theory]
    [InlineData("Offset", 0, 0)]
    public void Test_Clamped_Props_Int(string propName, int min, int max)
    {
      var queryParams = new QueryParamsModel();
      var prop = queryParams.GetType().GetProperty(propName);

      prop.SetValue(queryParams, -99999999);
      Assert.Equal(min, prop.GetValue(queryParams));

      if (min != max)
      {
        prop.SetValue(queryParams, 99999999);
        Assert.Equal(max, prop.GetValue(queryParams));
      }
    }

    [Fact]
    public void Test_Limit()
    {
      var queryParams = new QueryParamsModel();
      queryParams.Limit = -1;
      Assert.Equal(50, queryParams.Limit);

      queryParams.Limit = 0;
      Assert.Equal(50, queryParams.Limit);

      queryParams.Limit = 999;
      Assert.Equal(999, queryParams.Limit);
    }

    [Fact]
    public void Test_SortOrder()
    {
      var queryParams = new QueryParamsModel();
      Assert.Equal("asc", queryParams.SortOrder);

      queryParams.SortOrder = "desc";
      Assert.Equal("desc", queryParams.SortOrder);

      queryParams.SortOrder = "invalid";
      Assert.Equal("asc", queryParams.SortOrder);

      queryParams.SortOrder = null;
      Assert.Equal("asc", queryParams.SortOrder);
    }

    [Theory]
    [InlineData("SortKey")]
    public void Test_Trivial_String_Props(string propName)
    {
      var queryParams = new LodgingQueryParamsModel();
      var prop = queryParams.GetType().GetProperty(propName);
      prop.SetValue(queryParams, "test");
      Assert.Equal("test", prop.GetValue(queryParams));
    }
  }
}
