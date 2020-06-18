using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  /// <summary>
  /// A user review.
  /// </summary>
  public class ReviewModel : IValidatableObject
  {
    public int Id { get; set; }

    public int AccountId { get; set; }

    public string Comment { get; set; }

    public DateTime DateCreated { get; set; }

    [Range(0, 10)]
    public double Rating { get; set; }

    public int? LodgingId { get; set; }
    public LodgingModel Lodging { get; set; }

    /// <summary>
    /// Represents the _Review_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
