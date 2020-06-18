using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  /// <summary>
  /// Represents a bathroom.
  /// </summary>
  public class BathroomModel : IValidatableObject
  {
    public int Id { get; set; }

    public double Fixture { get; set; }

    /// <summary>
    /// Represents the _Bathroom_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
