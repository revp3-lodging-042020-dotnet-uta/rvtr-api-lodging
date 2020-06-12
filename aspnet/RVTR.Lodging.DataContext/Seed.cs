using System.Linq;
using System.Collections.Generic;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.DataContext
{
    public class Seed
    {
        public static void SeedDatabase(LodgingContext context)
        {
            if (context.Lodgings.Count() > 0) return;
            if (context.Rentals.Count() > 0) return;
            if (context.Reviews.Count() > 0) return;

            var lodging = new LodgingModel
            {
                Name = "lodging name",
                Location = new LocationModel
                {
                    Latitude = "latitude",
                    Address = new AddressModel
                    {
                        City = "city",
                    },
                },
                Reviews = new List<ReviewModel>() {
                    new ReviewModel {
                        Comment = "a comment",
                    }
                },
                Rentals = new List<RentalModel>() {
                    new RentalModel {
                        Name = "rental model",
                        RentalUnit = new RentalUnitModel {
                            Name = "unit model",
                            Rental = new RentalModel {
                                Name = "rental model"
                            },
                            Bedrooms = new List<BedroomModel>() {
                                new BedroomModel {
                                    BedType = "a bed"
                                }
                            },
                            Images = new List<ImageModel>() {
                                new ImageModel {
                                    Image = "this is invalid data, but would normally be a Base64 encoded string"
                                }
                            }
                        }
                    }
                }
            };

            context.Add(lodging);
            context.SaveChanges();
        }
    }
}
