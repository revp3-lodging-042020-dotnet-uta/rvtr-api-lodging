using System;
using System.Collections.Generic;
using RVTR.Lodging.ObjectModel.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace IntegrationTests
{
  public static class StaticTestingData
  {
    public static List<object[]> GetRequests =>
        new List<object[]>
        {
            new object[] { "api/v0.0/review" },
            new object[] { "api/v0.0/review/1" },
            new object[] { "api/v0.0/lodging" },
            new object[] { "api/v0.0/lodging/1" },
            new object[] { "api/v0.0/rental" },
            new object[] { "api/v0.0/rental/1" },
        };

    public static List<object[]> DeleteRequests =>
      new List<object[]>
      {
            new object[] { "api/v0.0/review/1" },
            new object[] { "api/v0.0/lodging/1" },
            new object[] { "api/v0.0/rental/1" },
      };
    public static List<object[]> PostRequests =>
      new List<object[]>
      {
          new object[] {"/api/v0.0/review", JObject.FromObject(new ReviewModel()
          {
            Id=1,
            AccountId=1,
            Comment="Test Comment",
            DateCreated=DateTime.Now,
            Rating=5,
            LodgingId=1
          }),
            new object[] {"/api/v0.0/rental", JObject.FromObject(new RentalModel()
          {
           Description = "Lodge in the Adirondaks",
           Id = 1,
           LodgingId = 1,
           Name = "Lodge Rental",
            RentalUnit = new RentalUnitModel() {
                            Name = "Rental Unit 1",
                            Description = "Unit description",
                            Occupancy = 4,
                            RentalUnitType = "Rental unit", }
            

          })
          },
          new object[] {"/api/v0.0/lodging", JObject.FromObject(new LodgingModel()
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
                  new AmenityModel() { Amenity = "Wifi" },
                  new AmenityModel() { Amenity = "Coffee" },
                  new AmenityModel() { Amenity = "Pool" },
              },
              Images = new List<ImageModel>() {
                  new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/seed/lodge-12.jpg" },
                  new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/seed/cabin-01.jpg" },
                  new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/seed/cabin-02.jpg" },
                  new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/seed/cabin-03.jpg" },
                  new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/seed/cabin-04.jpg" },
                  new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/seed/cabin-05.jpg" },
                  new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/seed/cabin-06.jpg" },
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
                                  BedType = new BedTypeModel() { BedType = "King" },
                                  BedCount = 2,
                                  RoomNumber = "100",
                                  Amenities = new List<AmenityModel>() {
                                      new AmenityModel() { Amenity = "Coffee" },
                                      new AmenityModel() { Amenity = "Wifi" },
                                      new AmenityModel() { Amenity = "Pool" },
                                  },
                                  Images = new List<ImageModel>() {
                                      new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/square/cabin-05-square.jpg" },
                                  }
                              },
                              new BedroomModel() {
                                  BedType = new BedTypeModel() { BedType = "King" },
                                  BedCount = 2,
                                  RoomNumber = "101",
                                  Amenities = new List<AmenityModel>() {
                                      new AmenityModel() { Amenity = "Coffee" },
                                      new AmenityModel() { Amenity = "Wifi" },
                                      new AmenityModel() { Amenity = "Pool" },
                                  },
                                  Images = new List<ImageModel>() {
                                      new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/square/cabin-06-square.jpg" },
                                  }
                              }
                          },
                          Bathrooms = new List<BathroomModel>() {
                              new BathroomModel() { Fixture = 1 },
                              new BathroomModel() { Fixture = 2 },
                          },
                      },
                  },
                  new RentalModel() {
                      RentalUnit = new RentalUnitModel() {
                          Name = "Rental Unit 2",
                          Description = "Unit for rent",
                          Occupancy = 3,
                          RentalUnitType = "Rental unit",
                          Bedrooms = new List<BedroomModel>() {
                              new BedroomModel() {
                                  BedType = new BedTypeModel() { BedType = "Queen" },
                                  BedCount = 2,
                                  RoomNumber = "200",
                                  Amenities = new List<AmenityModel>() {
                                      new AmenityModel() { Amenity = "Coffee" },
                                      new AmenityModel() { Amenity = "Wifi" },
                                      new AmenityModel() { Amenity = "Pool" },
                                  },
                                  Images = new List<ImageModel>() {
                                      new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/square/cabin-01-square.jpg" },
                                  }
                              },
                              new BedroomModel() {
                                  BedType = new BedTypeModel() { BedType = "King" },
                                  BedCount = 2,
                                  RoomNumber = "201",
                                  Amenities = new List<AmenityModel>() {
                                      new AmenityModel() { Amenity = "Coffee" },
                                      new AmenityModel() { Amenity = "Wifi" },
                                      new AmenityModel() { Amenity = "Pool" },
                                  },
                                  Images = new List<ImageModel>() {
                                      new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/square/cabin-03-square.jpg" },
                                  }
                              }
                          },
                          Bathrooms = new List<BathroomModel>() {
                              new BathroomModel() { Fixture = 1 },
                              new BathroomModel() { Fixture = 2 },
                          },
                      },
                  },
                  new RentalModel() {
                      RentalUnit = new RentalUnitModel() {
                          Name = "Rental Unit 3",
                          Description = "Unit for rent",
                          Occupancy = 3,
                          RentalUnitType = "Rental unit",
                          Bedrooms = new List<BedroomModel>() {
                              new BedroomModel() {
                                  BedType = new BedTypeModel() { BedType = "King" },
                                  BedCount = 1,
                                  RoomNumber = "201",
                                  Amenities = new List<AmenityModel>() {
                                      new AmenityModel() { Amenity = "Coffee" },
                                      new AmenityModel() { Amenity = "Wifi" },
                                      new AmenityModel() { Amenity = "Pool" },
                                  },
                                  Images = new List<ImageModel>() {
                                      new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/square/cabin-02-square.jpg" },
                                  }
                              },
                              new BedroomModel() {
                                  BedType = new BedTypeModel() { BedType = "Queen" },
                                  BedCount = 2,
                                  RoomNumber = "202",
                                  Amenities = new List<AmenityModel>() {
                                      new AmenityModel() { Amenity = "Coffee" },
                                      new AmenityModel() { Amenity = "Pool" },
                                  },
                                  Images = new List<ImageModel>() {
                                      new ImageModel() { Image = "https://p3lodging.blob.core.windows.net/p3lodging/square/cabin-04-square.jpg" },
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
              }
          })
          }
      }
    };
  }
}
