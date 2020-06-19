using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  /// <summary>
  /// Types of beds in a bedroom.
  /// </summary>
  public static class BedType
  {
    public const string King = "King";
    public const string Queen = "Queen";


  }

  /// <summary>
  /// Represents a type of bed.
  /// </summary>
  public class BedTypeModel : IValidatableObject
  {
    public int Id { get; set; }

    public string BedType { get; set; }

    /// <summary>
    /// Represents the _BedTypeModel_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
