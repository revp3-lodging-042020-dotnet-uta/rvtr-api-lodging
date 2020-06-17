using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  public class AmenityType
  {
    public const string Coffee = "Coffee";
    public const string Wifi = "Wifi";
    public const string Pool = "Pool";
  }

  /// <summary>
  /// Represents the _Amenity_ model
  /// </summary>
  public class AmenityModel : IValidatableObject
  {
    public int Id { get; set; }

    public string Amenity { get; set; }


    /// <summary>
    /// Represents the _AmenityModel_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
