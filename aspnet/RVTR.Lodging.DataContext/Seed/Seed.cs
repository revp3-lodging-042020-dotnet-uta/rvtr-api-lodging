using System.Linq;
using System.Collections.Generic;
using RVTR.Lodging.ObjectModel.Models;
using System;

namespace RVTR.Lodging.DataContext
{
    public class Seed
    {
        public static void SeedDatabase(LodgingContext context)
        {
            if (context.Lodgings.Count() > 0) return;
            if (context.Rentals.Count() > 0) return;
            if (context.Reviews.Count() > 0) return;

            var lodging1 = new LodgingModel
            {
                Name = "Quiet Forest Lodge",
                Description = "A quiet lodge with a nearby forest. Great for outdoor activities.",
                Location = new LocationModel() {
                    Address = new AddressModel() {
                        City = "Buena Vista",
                        Country = "United States",
                        PostalCode = "81211",
                        StateProvince = "CO",
                        Street = "30500 Co Rd 383",
                    },
                    Latitude = 0,
                    Longitude = 0,
                },
                Amenities = new List<AmenityModel>() {
                    new AmenityModel() { Amenity = AmenityType.Wifi },
                    new AmenityModel() { Amenity = AmenityType.Coffee },
                    new AmenityModel() { Amenity = AmenityType.Pool },
                },
                Images = new List<ImageModel>() {
                    new ImageModel() { Image = SeedImages.Images()[0] },
                    new ImageModel() { Image = SeedImages.Images()[1] },
                    new ImageModel() { Image = SeedImages.Images()[2] },
                    new ImageModel() { Image = SeedImages.Images()[3] },
                    new ImageModel() { Image = SeedImages.Images()[4] },
                    new ImageModel() { Image = SeedImages.Images()[5] },
                    new ImageModel() { Image = SeedImages.Images()[6] },
                },
                Rentals = new List<RentalModel>() {
                    new RentalModel() {
                        RentalUnit = new RentalUnitModel() {
                            Name = "Rental Unit 1",
                            Description = "Unit for rent",
                            Occupancy = 3,
                            RentalUnitType = "Rental unit",
                            Bedrooms = new List<BedroomModel>() {
                                new BedroomModel() {
                                    BedType = BedType.King,
                                    BedCount = 2,
                                    RoomNumber = "100",
                                    Amenities = new List<AmenityModel>() {
                                        new AmenityModel() { Amenity = AmenityType.Coffee },
                                        new AmenityModel() { Amenity = AmenityType.Wifi },
                                        new AmenityModel() { Amenity = AmenityType.Pool },
                                    },
                                    Images = new List<ImageModel>() {
                                        new ImageModel() { Image = SeedImages.Images()[22] },
                                    }
                                },
                                new BedroomModel() {
                                    BedType = BedType.King,
                                    BedCount = 2,
                                    RoomNumber = "101",
                                    Amenities = new List<AmenityModel>() {
                                        new AmenityModel() { Amenity = AmenityType.Coffee },
                                        new AmenityModel() { Amenity = AmenityType.Wifi },
                                        new AmenityModel() { Amenity = AmenityType.Pool },
                                    },
                                    Images = new List<ImageModel>() {
                                        new ImageModel() { Image = SeedImages.Images()[24] },
                                    }
                                }
                            },
                            Bathrooms = new List<BathroomModel>() {
                                new BathroomModel() { Fixture = 1 },
                                new BathroomModel() { Fixture = 2 },
                            },
                        },
                    }
                },
                Reviews = new List<ReviewModel>() {
                    new ReviewModel() {
                        AccountId = 1,
                        Comment = "This lodge is fantastic!",
                        DateCreated = DateTime.Now,
                        Rating = 9,
                    },
                    new ReviewModel() {
                        AccountId = 2,
                        Comment = "I had fun.",
                        DateCreated = DateTime.Now,
                        Rating = 7,
                    },
                    new ReviewModel() {
                        AccountId = 3,
                        Comment = "The nearby forest is great.",
                        DateCreated = DateTime.Now,
                        Rating = 8.5,
                    },
                    new ReviewModel() {
                        AccountId = 4,
                        Comment = "I only came here for coffee.",
                        DateCreated = DateTime.Now,
                        Rating = 5,
                    }
                },
            };

            var lodging2 = new LodgingModel
            {
                Name = "Outdoor Adventures",
                Description = "A fun place to camp for families.",
                Location = new LocationModel() {
                    Address = new AddressModel() {
                        City = "Alma",
                        Country = "United States",
                        PostalCode = "80420",
                        StateProvince = "CO",
                        Street = "Co Rd 19",
                    },
                    Latitude = 0,
                    Longitude = 0,
                },
                Amenities = new List<AmenityModel>() {
                    new AmenityModel() { Amenity = AmenityType.Wifi },
                    new AmenityModel() { Amenity = AmenityType.Coffee },
                    new AmenityModel() { Amenity = AmenityType.Pool },
                },
                Images = new List<ImageModel>() {
                    new ImageModel() { Image = SeedImages.Images()[7] },
                    new ImageModel() { Image = SeedImages.Images()[8] },
                    new ImageModel() { Image = SeedImages.Images()[9] },
                    new ImageModel() { Image = SeedImages.Images()[10] },
                    new ImageModel() { Image = SeedImages.Images()[11] },
                    new ImageModel() { Image = SeedImages.Images()[12] },
                    new ImageModel() { Image = SeedImages.Images()[13] },
                },
                Rentals = new List<RentalModel>() {
                    new RentalModel() {
                        RentalUnit = new RentalUnitModel() {
                            Name = "Rental Unit 1",
                            Description = "Unit for rent",
                            Occupancy = 3,
                            RentalUnitType = "Rental unit",
                            Bedrooms = new List<BedroomModel>() {
                                new BedroomModel() {
                                    BedType = BedType.Queen,
                                    BedCount = 1,
                                    RoomNumber = "200",
                                    Amenities = new List<AmenityModel>() {
                                        new AmenityModel() { Amenity = AmenityType.Wifi },
                                        new AmenityModel() { Amenity = AmenityType.Pool },
                                    },
                                    Images = new List<ImageModel>() {
                                        new ImageModel() { Image = SeedImages.Images()[24] },
                                    }
                                },
                                new BedroomModel() {
                                    BedType = BedType.King,
                                    BedCount = 2,
                                    RoomNumber = "202",
                                    Amenities = new List<AmenityModel>() {
                                        new AmenityModel() { Amenity = AmenityType.Coffee },
                                        new AmenityModel() { Amenity = AmenityType.Wifi },
                                        new AmenityModel() { Amenity = AmenityType.Pool },
                                    },
                                    Images = new List<ImageModel>() {
                                        new ImageModel() { Image = SeedImages.Images()[2] },
                                    }
                                }
                            },
                            Bathrooms = new List<BathroomModel>() {
                                new BathroomModel() { Fixture = 1 },
                                new BathroomModel() { Fixture = 2 },
                            },
                        },
                    }
                },
                Reviews = new List<ReviewModel>() {
                    new ReviewModel() {
                        AccountId = 1,
                        Comment = "My family had a great time here.",
                        DateCreated = DateTime.Now,
                        Rating = 8,
                    },
                    new ReviewModel() {
                        AccountId = 2,
                        Comment = "My mom made me go and I hated it.",
                        DateCreated = DateTime.Now,
                        Rating = 1,
                    },
                    new ReviewModel() {
                        AccountId = 3,
                        Comment = "This is a fun place",
                        DateCreated = DateTime.Now,
                        Rating = 7,
                    },
                    new ReviewModel() {
                        AccountId = 4,
                        Comment = "Great if you have small children.",
                        DateCreated = DateTime.Now,
                        Rating = 8,
                    }
                },
            };

            var lodging3 = new LodgingModel
            {
                Name = "Epic Camp",
                Description = "Epic Camp offers the most fantastic outdoor experience!",
                Location = new LocationModel() {
                    Address = new AddressModel() {
                        City = "Buena Vista",
                        Country = "United States",
                        PostalCode = "81211",
                        StateProvince = "CO",
                        Street = "27700 County Road",
                    },
                    Latitude = 0,
                    Longitude = 0,
                },
                Amenities = new List<AmenityModel>() {
                    new AmenityModel() { Amenity = AmenityType.Wifi },
                    new AmenityModel() { Amenity = AmenityType.Coffee },
                    new AmenityModel() { Amenity = AmenityType.Pool },
                },
                Images = new List<ImageModel>() {
                    new ImageModel() { Image = SeedImages.Images()[14] },
                    new ImageModel() { Image = SeedImages.Images()[15] },
                    new ImageModel() { Image = SeedImages.Images()[16] },
                    new ImageModel() { Image = SeedImages.Images()[17] },
                    new ImageModel() { Image = SeedImages.Images()[18] },
                    new ImageModel() { Image = SeedImages.Images()[19] },
                    new ImageModel() { Image = SeedImages.Images()[20] },
                },
                Rentals = new List<RentalModel>() {
                    new RentalModel() {
                        RentalUnit = new RentalUnitModel() {
                            Name = "Rental Unit 1",
                            Description = "Unit for rent",
                            Occupancy = 3,
                            RentalUnitType = "Rental unit",
                            Bedrooms = new List<BedroomModel>() {
                                new BedroomModel() {
                                    BedType = BedType.King,
                                    BedCount = 3,
                                    RoomNumber = "105",
                                    Amenities = new List<AmenityModel>() {
                                        new AmenityModel() { Amenity = AmenityType.Coffee },
                                        new AmenityModel() { Amenity = AmenityType.Wifi },
                                        new AmenityModel() { Amenity = AmenityType.Pool },
                                    },
                                    Images = new List<ImageModel>() {
                                        new ImageModel() { Image = SeedImages.Images()[21] },
                                    }
                                },
                                new BedroomModel() {
                                    BedType = BedType.King,
                                    BedCount = 2,
                                    RoomNumber = "106",
                                    Amenities = new List<AmenityModel>() {
                                        new AmenityModel() { Amenity = AmenityType.Coffee },
                                        new AmenityModel() { Amenity = AmenityType.Wifi },
                                        new AmenityModel() { Amenity = AmenityType.Pool },
                                    },
                                    Images = new List<ImageModel>() {
                                        new ImageModel() { Image = SeedImages.Images()[6] },
                                    }
                                }
                            },
                            Bathrooms = new List<BathroomModel>() {
                                new BathroomModel() { Fixture = 1 },
                                new BathroomModel() { Fixture = 2 },
                            },
                        },
                    }
                },
                Reviews = new List<ReviewModel>() {
                    new ReviewModel() {
                        AccountId = 1,
                        Comment = "It wasn't that epic.",
                        DateCreated = DateTime.Now,
                        Rating = 5,
                    },
                    new ReviewModel() {
                        AccountId = 2,
                        Comment = "I was expecting more.",
                        DateCreated = DateTime.Now,
                        Rating = 4,
                    },
                    new ReviewModel() {
                        AccountId = 3,
                        Comment = "Don't believe the hype.",
                        DateCreated = DateTime.Now,
                        Rating = 2,
                    },
                    new ReviewModel() {
                        AccountId = 4,
                        Comment = "ITS EPIC!1!!",
                        DateCreated = DateTime.Now,
                        Rating = 10,
                    }
                },
            };

            var lodging4 = new LodgingModel
            {
                Name = "The Woodsman",
                Description = "The next level in camping outdoors.",
                Location = new LocationModel() {
                    Address = new AddressModel() {
                        City = "Buena Vista",
                        Country = "United States",
                        PostalCode = "81211",
                        StateProvince = "CO",
                        Street = "33975 US Hwy 24 N",
                    },
                    Latitude = 0,
                    Longitude = 0,
                },
                Amenities = new List<AmenityModel>() {
                    new AmenityModel() { Amenity = AmenityType.Wifi },
                    new AmenityModel() { Amenity = AmenityType.Coffee },
                    new AmenityModel() { Amenity = AmenityType.Pool },
                },
                Images = new List<ImageModel>() {
                    new ImageModel() { Image = SeedImages.Images()[21] },
                    new ImageModel() { Image = SeedImages.Images()[22] },
                    new ImageModel() { Image = SeedImages.Images()[23] },
                    new ImageModel() { Image = SeedImages.Images()[24] },
                    new ImageModel() { Image = SeedImages.Images()[25] },
                    new ImageModel() { Image = SeedImages.Images()[26] },
                    new ImageModel() { Image = SeedImages.Images()[27] },
                },
                Rentals = new List<RentalModel>() {
                    new RentalModel() {
                        RentalUnit = new RentalUnitModel() {
                            Name = "Rental Unit 1",
                            Description = "Unit for rent",
                            Occupancy = 3,
                            RentalUnitType = "Rental unit",
                            Bedrooms = new List<BedroomModel>() {
                                new BedroomModel() {
                                    BedType = BedType.King,
                                    BedCount = 3,
                                    RoomNumber = "10",
                                    Amenities = new List<AmenityModel>() {
                                        new AmenityModel() { Amenity = AmenityType.Coffee },
                                        new AmenityModel() { Amenity = AmenityType.Wifi },
                                    },
                                    Images = new List<ImageModel>() {
                                        new ImageModel() { Image = SeedImages.Images()[14] },
                                    }
                                },
                                new BedroomModel() {
                                    BedType = BedType.King,
                                    BedCount = 2,
                                    RoomNumber = "11",
                                    Amenities = new List<AmenityModel>() {
                                        new AmenityModel() { Amenity = AmenityType.Coffee },
                                        new AmenityModel() { Amenity = AmenityType.Wifi },
                                    },
                                    Images = new List<ImageModel>() {
                                        new ImageModel() { Image = SeedImages.Images()[9] },
                                    }
                                }
                            },
                            Bathrooms = new List<BathroomModel>() {
                                new BathroomModel() { Fixture = 1 },
                                new BathroomModel() { Fixture = 2 },
                            },
                        },
                    }
                },
                Reviews = new List<ReviewModel>() {
                    new ReviewModel() {
                        AccountId = 1,
                        Comment = "I had a good time.",
                        DateCreated = DateTime.Now,
                        Rating = 9,
                    },
                    new ReviewModel() {
                        AccountId = 2,
                        Comment = "This place changed my life.",
                        DateCreated = DateTime.Now,
                        Rating = 10,
                    },
                    new ReviewModel() {
                        AccountId = 3,
                        Comment = "It's good, but not next-level.",
                        DateCreated = DateTime.Now,
                        Rating = 7,
                    },
                    new ReviewModel() {
                        AccountId = 4,
                        Comment = "This is just like any other place.",
                        DateCreated = DateTime.Now,
                        Rating = 5,
                    }
                },
            };



            context.Add(lodging1);
            context.Add(lodging2);
            context.Add(lodging3);
            context.Add(lodging4);
            context.SaveChanges();
        }
    }
}
