using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.DataContext;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class SearchFilterTest
  {

    [Fact]
    public void Test_Limit_Clamp()
    {
      var filterModel = new SearchFilter();
      filterModel.Limit = -1;
      Assert.Equal(50, filterModel.Limit);

      filterModel.Limit = 0;
      Assert.Equal(50, filterModel.Limit);

      filterModel.Limit = 999;
      Assert.Equal(999, filterModel.Limit);
    }

    [Fact]
    public void Test_Paginate_Clamp()
    {
      var filterModel = new SearchFilter();
      filterModel.Paginate = -1;
      Assert.Equal(0, filterModel.Paginate);

      filterModel.Paginate = 999;
      Assert.Equal(999, filterModel.Paginate);
    }

    [Fact]
    public void Test_SortOrder_Keywords()
    {
      var filterModel = new SearchFilter();
      filterModel.SortOrder = null;
      Assert.Equal("asc", filterModel.SortOrder);

      filterModel.SortOrder = "nope";
      Assert.Equal("asc", filterModel.SortOrder);

      filterModel.SortOrder = "asc";
      Assert.Equal("asc", filterModel.SortOrder);

      filterModel.SortOrder = "desc";
      Assert.Equal("desc", filterModel.SortOrder);
    }

  }
}
