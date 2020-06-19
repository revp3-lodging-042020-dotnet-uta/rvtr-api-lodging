using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.ObjectModel.Models;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class AmenityModelTest
  {
    public static readonly IEnumerable<Object[]> _amenities = new List<Object[]>
    {
      new object[]
      {
        new AmenityModel()
        {
          Id = 0,
          Amenity = "amenity",
        }
      }
    };

    [Theory]
    [MemberData(nameof(_amenities))]
    public void Test_Create_AmenityModel(AmenityModel amenity)
    {
      var validationContext = new ValidationContext(amenity);
      var actual = Validator.TryValidateObject(amenity, validationContext, null, true);

      Assert.True(actual);
    }

    [Theory]
    [MemberData(nameof(_amenities))]
    public void Test_Validate_AddressModel(AmenityModel amenity)
    {
      var validationContext = new ValidationContext(amenity);

      Assert.Empty(amenity.Validate(validationContext));
    }
  }
}
