using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.ObjectModel.Models;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class BedroomModelTest
  {
    public static readonly IEnumerable<Object[]> _bedrooms = new List<Object[]>
    {
      new object[]
      {
        new BedroomModel()
        {
          Id = 0,
          BedType = "bed",
          Count = 1
        }
      }
    };

    [Theory]
    [MemberData(nameof(_bedrooms))]
    public void Test_Create_BedroomModel(BedroomModel bedroom)
    {
      var validationContext = new ValidationContext(bedroom);
      var actual = Validator.TryValidateObject(bedroom, validationContext, null, true);

      Assert.True(actual);
    }

    [Theory]
    [MemberData(nameof(_bedrooms))]
    public void Test_Validate_BedroomModel(BedroomModel bedroom)
    {
      var validationContext = new ValidationContext(bedroom);

      Assert.Empty(bedroom.Validate(validationContext));
    }
  }
}
