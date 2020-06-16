using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Location_ model
  /// </summary>
  public class LocationModel : IValidatableObject
  {
    public int Id { get; set; }

    public AddressModel Address { get; set; }

    [Range(-90, 90)]
    public double Latitude { get; set; }

    public string Locale { get; set; }

    [Range(-180, 180)]
    public double Longitude { get; set; }

    /// <summary>
    /// Represents the _Location_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
