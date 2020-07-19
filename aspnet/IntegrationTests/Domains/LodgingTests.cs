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
using Xunit;

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

    //[Theory]
    //[InlineData()]
    //public Task Post_LodgingControllerSuccessfullyCreatesDbEntry(string url)
    //{
    //  //Arrange

    //  //Act

    //  //Assert

    //}

    [Fact]
    public async Task Get_SuccessfullyRetrievesAllLodgingFromDbEntries()
    {

      //Arrange
      var uri = "api/lodging";
      var defaultPage = await _client.GetAsync(uri);

      //Act
      //var response = await _client.SendAsync(
      //  );

      //Assert
      Assert.Equals(HttpStatusCode.OK, defaultPage.StatusCode);
      //Assert.Equals(uri, response.Headers.Location.OriginalString);
    }

    //[Theory]
    //[InlineData(0)]
    //[InlineData(1)]
    //[InlineData(3)]
    //public Task Get_RetrievesLodgingFromId(int id)
    //{
    //  //Arrange

    //  //Act

    //  //Assert
    //}

    //[Theory]
    //[InlineData()]
    //public Task Delete_SuccessfullyRemovesLodgingFromDb(int id)
    //{
    //  //Arrange

    //  //Act

    //  //Assert
    //}
  }
}
