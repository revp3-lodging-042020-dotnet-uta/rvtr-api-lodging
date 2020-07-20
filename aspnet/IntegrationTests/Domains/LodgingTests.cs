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

    [Theory]
    [InlineData("api/v0.0/lodging")]
    public async Task Get_EndpointsReturnSuccess(string url)
    {
      //Arrange
      var response = await _client.GetAsync(url);

      //Act

      //Assert
      response.EnsureSuccessStatusCode();
      Xunit.Assert.Equal("text/html; charset=utf-8",
        response.Content.Headers.ContentType.ToString());
    }

    [Fact]
    public async Task Get_SuccessfullyRetrievesAllLodgingFromDbEntries()
    {
      //Arrange
      var response = await _client.GetAsync("api/v0.0/lodging");

      //Act

      //Assert
      response.EnsureSuccessStatusCode();
      Xunit.Assert.Equal("200", response.IsSuccessStatusCode.ToString());
    }

    [Theory]
    [InlineData(0)]
    public async Task Get_RetrievesLodgingFromId(int id)
    {
      //Arrange
      var response = await _client.GetAsync($"api/v0.0/lodging/{id}");
      //Act

      //Assert
      response.EnsureSuccessStatusCode();
      Xunit.Assert.Equal("200", response.IsSuccessStatusCode.ToString());

    }

    [Theory]
    [InlineData(0)]
    public async Task Delete_SuccessfullyRemovesLodgingFromDb(int id)
    {
      //Arrange
      var response = await _client.GetAsync($"api/v0.0/lodging/delete/{id}");

      //Act

      //Assert
      response.EnsureSuccessStatusCode();
      Xunit.Assert.Equal("201", response.IsSuccessStatusCode.ToString());
    }
  }
}
