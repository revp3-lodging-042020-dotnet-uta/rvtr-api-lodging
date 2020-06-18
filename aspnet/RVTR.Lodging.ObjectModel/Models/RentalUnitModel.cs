using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  /// <summary>
  /// Represents the _RentalUnit_ model
  /// </summary>
  public class RentalUnitModel : IValidatableObject
  {
    public int Id { get; set; }

    public IEnumerable<BathroomModel> Bathrooms { get; set; }

    public IEnumerable<BedroomModel> Bedrooms { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    [Range(1, 10)]
    public int Occupancy { get; set; }

    public string RentalUnitType { get; set; }

    public int? RentalId { get; set; }
    public RentalModel Rental { get; set; }

    /// <summary>
    /// Represents the _RentalUnit_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
