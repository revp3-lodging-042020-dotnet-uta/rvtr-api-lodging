using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RVTR.Lodging.WebApi;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
//using Xunit;

namespace IntegrationTests.Domains
{

  [TestClass]
  public class LodgingTests : IClassFixture<CustomWebApplicationFactoryInMemDB<Startup>>
  {
    private readonly CustomWebApplicationFactoryInMemDB<Startup> _factory;
    private readonly HttpClient _client;

    public LodgingTests(CustomWebApplicationFactoryInMemDB<Startup> factory)
    {
      _factory = factory;
      _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
          AllowAutoRedirect = false
        });
    }

    //public Task Post_LodgingControllerSuccessfullyCreatesDbEntry(string url)
    //{
    //  //Arrange

    //  //Act

    //  //Assert

    //}

    [TestMethod]
    public async Task Get_EndpointsReturnSuccess(string url = "api/v0.0/lodging")
    {
      //Arrange
      var response = await _client.GetAsync(url);

      //Act

      //Assert
      response.EnsureSuccessStatusCode();
      Assert.Equals("text/html; charset=utf-8",
        response.Content.Headers.ContentType.ToString());
    }

    //[Fact]
    //public async Task Get_SuccessfullyRetrievesAllLodgingFromDbEntries()
    //{

    //  //Arrange

    //  //Act

    //  //Assert
    //}

    //public Task Get_RetrievesLodgingFromId(int id)
    //{
    //  //Arrange

    //  //Act

    //  //Assert
    //}

    //public Task Delete_SuccessfullyRemovesLodgingFromDb(int id)
    //{
    //  //Arrange

    //  //Act

    //  //Assert
    //}
  }
}
