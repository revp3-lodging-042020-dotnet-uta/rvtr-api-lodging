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
                    Latitude = 14,
                    Address = new AddressModel
                    {
                        City = "city",
                    },
                },
                Reviews = new List<ReviewModel>() {
                    new ReviewModel {
                        Comment = "a comment",
                        Rating = 1
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

            var lodging2 = new LodgingModel
            {
                Name = "another lodging",
                Location = new LocationModel
                {
                    Latitude = 15,
                    Address = new AddressModel
                    {
                        City = "city 2",
                    },
                },
                Reviews = new List<ReviewModel>() {
                    new ReviewModel {
                        Comment = "another comment",
                        Rating = 1,
                    },
                    new ReviewModel {
                        Comment = "a second comment",
                        Rating = 4,
                    },
                    new ReviewModel {
                        Comment = "wow three comments!",
                        Rating = 3,
                    }
                },
                Rentals = new List<RentalModel>() {
                    new RentalModel {
                        Name = "rental model 2",
                        RentalUnit = new RentalUnitModel {
                            Name = "unit model 2",
                            Rental = new RentalModel {
                                Name = "rental model 2"
                            },
                            Bathrooms = new List<BathroomModel>()
                            {
                                new BathroomModel {
                                    Fixture = 2
                                }
                            },
                            Bedrooms = new List<BedroomModel>() {
                                new BedroomModel {
                                    BedType = "a bed 2"
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

            var lodging3 = new LodgingModel
            {
                Name = "a third lodging",
                Location = new LocationModel
                {
                    Latitude = 90,
                    Address = new AddressModel
                    {
                        City = "city 3",
                    },
                },
                Reviews = new List<ReviewModel>() {
                    new ReviewModel {
                        Comment = "a single comment",
                        Rating = 4,
                    },
                    new ReviewModel {
                        Comment = "comment the second",
                        Rating = 3,
                    }
                },
                Rentals = new List<RentalModel>() {
                    new RentalModel {
                        Name = "rental model 3",
                        RentalUnit = new RentalUnitModel {
                            Name = "unit model 3",
                            Rental = new RentalModel {
                                Name = "rental model 3"
                            },
                            Bedrooms = new List<BedroomModel>() {
                                new BedroomModel {
                                    BedType = "bed is comfy"
                                },
                                new BedroomModel {
                                    BedType = "wow two beds"
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
            context.Add(lodging2);
            context.Add(lodging3);
            context.SaveChanges();
        }
    }
}
