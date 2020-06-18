using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  /// <summary>
  /// Reference to an image.
  /// </summary>
  public class ImageModel : IValidatableObject
  {
    public int Id { get; set; }

    /// <summary>
    /// The URL to an image.
    /// </summary>
    /// <value></value>
    public string Image { get; set; }

    /// <summary>
    /// Represents the _ImageModel_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
