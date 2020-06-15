using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.ObjectModel.Models;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class ImageModelTest
  {
    public static readonly IEnumerable<Object[]> _images = new List<Object[]>
    {
      new object[]
      {
        new ImageModel()
        {
          Id = 0,
          Image = "someRandomImage.jpg"
        }
      }
    };

    [Theory]
    [MemberData(nameof(_images))]
    public void Test_Create_ImageModel(ImageModel image)
    {
      var validationContext = new ValidationContext(image);
      var actual = Validator.TryValidateObject(image, validationContext, null, true);

      Assert.True(actual);
    }

    [Theory]
    [MemberData(nameof(_images))]
    public void Test_Validate_ImageModel(ImageModel image)
    {
      var validationContext = new ValidationContext(image);

      Assert.Empty(image.Validate(validationContext));
    }
  }
}
