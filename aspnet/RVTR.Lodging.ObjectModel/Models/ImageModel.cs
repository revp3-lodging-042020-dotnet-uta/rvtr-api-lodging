using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  /// <summary>
  /// Represents the _RentalUnit_ model
  /// </summary>
  public class ImageModel : IValidatableObject
  {
    public int Id { get; set; }

    /// <summary>
    /// Images are store in Base64 encoded strings.
    /// </summary>
    public string Image { get; set; }

    /// <summary>
    /// Represents the _RentalImageModel_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
