using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.DataContext
{
  public static class SeedData
  {
    public static void Seed(this ModelBuilder modelBuilder)
    {
      #region LodgingModel
      var rentals = new List<RentalModel>();
      rentals.Add(new RentalModel{Id = 1,Lodging = new LodgingModel{Id = 1},
        Name = "Rental One",RentalUnit = new RentalUnitModel{Bathrooms
          = new List<BathroomModel>(),Occupancy = 3, RentalUnitType = "random",RentalId = 1}});

      var reviews = new List<ReviewModel>();
      reviews.Add(new ReviewModel{AccountId = 1,Comment = "Horrible Place Hate It",DateCreated = DateTime.Today,Id = 1,LodgingId = 1,Rating =1});
      modelBuilder.Entity<LodgingModel>().HasData(
        new LodgingModel {Id = 1,Name = "Lost In The Woods",Location
          = new LocationModel{Id = 1,Address
          = new AddressModel{Id = 1,City = "Easley",LocationId = 1,PostalCode = "29642",StateProvince = "SC",Street = "244 Jenny Ln"},Latitude = "34.886110",Longitude = "-82.574880",Locale = "Rural"},
          Rentals = rentals,
        }
      );

      #endregion

      #region ReviewModel
      modelBuilder.Entity<ReviewModel>().HasData(
        new ReviewModel{Id = 1,AccountId = 1,Comment = "Worst Ever",DateCreated = DateTime.Today,LodgingId = 1,Rating = 1},
       new ReviewModel { Id = 2, AccountId = 1, Comment = "Hated It", DateCreated = DateTime.Today, LodgingId = 1, Rating = 1 },
       new ReviewModel { Id = 3, AccountId = 1, Comment = "Never Again", DateCreated = DateTime.Today, LodgingId = 1, Rating = 1 },
        new ReviewModel { Id = 4, AccountId = 1, Comment = "Hostess is Horrible", DateCreated = DateTime.Today, LodgingId = 1, Rating = 1 }
        );

      #endregion

      #region RentalModel

      modelBuilder.Entity<RentalModel>().HasData(
        new RentalModel {Id = 1, LodgingId = 1, Name = "Name Here"}
      );


      #endregion




    }
  }
}
