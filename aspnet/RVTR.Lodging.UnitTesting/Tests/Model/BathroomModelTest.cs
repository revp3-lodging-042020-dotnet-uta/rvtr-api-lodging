using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.ObjectModel.Models;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class BathroomModelTest
  {
    public static readonly IEnumerable<Object[]> _bathrooms = new List<Object[]>
    {
      new object[]
      {
        new BathroomModel()
        {
          Id = 0,
          Fixture = 1.0
        }
      }
    };

    [Theory]
    [MemberData(nameof(_bathrooms))]
    public void Test_Create_BathroomModel(BathroomModel bathroom)
    {
      var validationContext = new ValidationContext(bathroom);
      var actual = Validator.TryValidateObject(bathroom, validationContext, null, true);

      Assert.True(actual);
    }

    [Theory]
    [MemberData(nameof(_bathrooms))]
    public void Test_Validate_BathroomModel(BathroomModel bathroom)
    {
      var validationContext = new ValidationContext(bathroom);

      Assert.Empty(bathroom.Validate(validationContext));
    }
  }
}
