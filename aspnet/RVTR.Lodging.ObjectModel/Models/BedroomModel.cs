using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  /// <summary>
  /// Represents a bedroom.
  /// </summary>
  public class BedroomModel : IValidatableObject
  {
    public int Id { get; set; }

    public BedTypeModel BedType { get; set; }

    [Range(0, 10)]
    public int BedCount { get; set; }

    public string RoomNumber { get; set; }

    public IEnumerable<ImageModel> Images { get; set; }

    public IEnumerable<AmenityModel> Amenities { get; set; }

    /// <summary>
    /// Represents the _Bedroom_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
