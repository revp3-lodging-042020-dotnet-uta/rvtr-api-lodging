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
          }
      }
    };
  }
}
