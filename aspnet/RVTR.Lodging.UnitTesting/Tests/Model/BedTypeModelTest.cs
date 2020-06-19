using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.ObjectModel.Models;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class BedTypeModelTest
  {
    public static readonly IEnumerable<Object[]> _bedtypes = new List<Object[]>
    {
      new object[]
      {
        new BedTypeModel()
        {
          Id = 0,
          BedType = BedType.King,
        }
      }
    };

    [Theory]
    [MemberData(nameof(_bedtypes))]
    public void Test_Create_BedroomModel(BedTypeModel bedType)
    {
      var validationContext = new ValidationContext(bedType);
      var actual = Validator.TryValidateObject(bedType, validationContext, null, true);

      Assert.True(actual);
    }

    [Theory]
    [MemberData(nameof(_bedtypes))]
    public void Test_Validate_BedroomModel(BedTypeModel bedType)
    {
      var validationContext = new ValidationContext(bedType);

      Assert.Empty(bedType.Validate(validationContext));
    }
  }
}
