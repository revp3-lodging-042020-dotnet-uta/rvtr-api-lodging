using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using RVTR.Lodging.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using IntegrationTests.Utilities.Interfaces;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RVTR.Lodging.ObjectModel.Models;

namespace IntegrationTests.Domains
{

  //[TestClass]

  public class RentalTests :
    IClassFixture<CustomWebApplicationFactoryInMemDB<Startup>>
    {
      //private readonly TestServer _server;
      private readonly CustomWebApplicationFactoryInMemDB<Startup> _factory;
      private readonly HttpClient _client;

      public RentalTests(CustomWebApplicationFactoryInMemDB<Startup> factory)
      {
        //arrange
        _factory = factory;
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
          AllowAutoRedirect = false
        });
      }

      //[TestMethod]
      [Fact]

      public async Task Get_AllRentals_ReturnsOK()
      {
        // Act
        var response = await _client.GetAsync("api/v0.0/rental");


        //var responseString = await response.Content.ReadAsStringAsync();
        //var responseString = await response.StatusCode.

        // Assert
        Assert.Equal(HttpStatusCode.OK,
            response.StatusCode);


      }
      //testing if removing rental returns 204
      //[TestMethod]
      [Fact]
      public async Task Remove_Rental_Returns204()
      {
        // Act
        var response = await _client.DeleteAsync("/api/v{version:apiVersion}/RentalController/1");


        // Assert
        Assert.Equal(HttpStatusCode.NoContent,
            response.StatusCode);
      }


      //Testing Get Id successs: Return (200) / If there is not a rental under the entered id -> RETURN 404(Not found)

      public async Task Get_RentalById_ReturnsOK()
      {
        //act
        var response = await _client.GetAsync("/api/v{version:apiVersion}/RentalController/1");
        var invalidRentalReq = await _client.GetAsync("/api/v{version:apiVersion}/RentalController/-1");

        //assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, invalidRentalReq.StatusCode);

      }

      //    POST ​/0​/Rental : 
      //Return 201(Created)
      //If a rental is created with the same Id as a preexisting Rental -> Return 409(Conflict)

      public async Task Post_Rental_ReturnsOK()
      {
        //act
        var response = await _client.PostAsync("/api/v{version:apiVersion}/RentalController/",
          new StringContent(JsonConvert.SerializeObject(
          new RentalModel()
          {
            Description = "Large Cabin in the Adirondaks",
            Id = 1,
            Name = "Adr Rental",

          //need to figure out the difference between lodgingId and Id
          // need to figure out how that functions so I can write the 409 test
        }),
        Encoding.UTF8,
          "application/json"));



        //assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);


      }

      //create test Put_Rental_Returns204



    }



  }

