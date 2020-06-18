using RVTR.Lodging.DataContext;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
    public class QueryParamModelTest
  {

    [Fact]
    public void Test_Limit_Clamp()
    {
      var queryParams = new QueryParamModel();
      queryParams.Limit = -1;
      Assert.Equal(50, queryParams.Limit);

      queryParams.Limit = 0;
      Assert.Equal(50, queryParams.Limit);

      queryParams.Limit = 999;
      Assert.Equal(999, queryParams.Limit);
    }

    [Fact]
    public void Test_Offset_Clamp()
    {
      var queryParams = new QueryParamModel();
      queryParams.Offset = -1;
      Assert.Equal(0, queryParams.Offset);

      queryParams.Offset = 999;
      Assert.Equal(999, queryParams.Offset);
    }

    [Fact]
    public void Test_SortOrder_Keywords()
    {
      var queryParams = new QueryParamModel();
      queryParams.SortOrder = null;
      Assert.Equal("asc", queryParams.SortOrder);

      queryParams.SortOrder = "nope";
      Assert.Equal("asc", queryParams.SortOrder);

      queryParams.SortOrder = "asc";
      Assert.Equal("asc", queryParams.SortOrder);

      queryParams.SortOrder = "desc";
      Assert.Equal("desc", queryParams.SortOrder);
    }

  }
}
