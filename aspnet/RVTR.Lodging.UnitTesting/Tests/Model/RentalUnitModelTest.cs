using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.ObjectModel.Models;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class RentalUnitModelTest
  {
    public static readonly IEnumerable<Object[]> _rentalUnits = new List<Object[]>
    {
      new object[]
      {
        new RentalUnitModel()
        {
          Id = 0,
          Bathrooms = new List<BathroomModel>(),
          Bedrooms = new List<BedroomModel>(),
          Name = "name",
          Occupancy = 0,
          RentalUnitType = "unit",
          RentalId = 0,
          Rental = null
        }
      }
    };

    [Theory]
    [MemberData(nameof(_rentalUnits))]
    public void Test_Create_RentalUnitModel(RentalUnitModel rentalUnit)
    {
      var validationContext = new ValidationContext(rentalUnit);
      var actual = Validator.TryValidateObject(rentalUnit, validationContext, null, true);

      Assert.True(actual);
    }

    [Theory]
    [MemberData(nameof(_rentalUnits))]
    public void Test_Validate_RentalUnitModel(RentalUnitModel rentalUnit)
    {
      var validationContext = new ValidationContext(rentalUnit);

      Assert.Empty(rentalUnit.Validate(validationContext));
    }
  }
}
