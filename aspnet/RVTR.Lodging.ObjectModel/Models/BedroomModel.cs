using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  public class BedType
  {
    public const string King = "King";
    public const string Queen = "Queen";
  }

  /// <summary>
  /// Represents the _Bedroom_ model
  /// </summary>
  public class BedroomModel : IValidatableObject
  {
    public int Id { get; set; }

    public string BedType { get; set; }

    public int BedCount { get; set; }

    public string RoomNumber { get; set; }

    public IEnumerable<ImageModel> Images { get; set; }

    public IEnumerable<AmenityModel> Amenities { get; set; }

    [Range(0, 10)]
    public int Count { get; set; }

    /// <summary>
    /// Represents the _Bedroom_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
