using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.WebApi;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class QueryModelTest
  {
    public static readonly IEnumerable<Object[]> _Queries = new List<Object[]>
    {
      new object[]
      {
        new QueryModel()
        {
          Limit = 5,
          Paginate = 10,
          SortOrder = "asc"
        }
      }
    };

    [Theory]
    [MemberData(nameof(_Queries))]
    public void Test_Create_AddressModel(QueryModel query)
    {
      var validationContext = new ValidationContext(query);
      var actual = Validator.TryValidateObject(query, validationContext, null, true);

      Assert.True(actual);
    }
  }
}
