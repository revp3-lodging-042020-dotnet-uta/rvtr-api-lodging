using Microsoft.VisualStudio.TestTools.UnitTesting;
using RVTR.Lodging.WebApi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Domains
{
  [TestClass]
  public class LodgingTests : IClassFixture<CustomWebApplicationFactoryInMemDB<Startup>>
  {
    private readonly CustomWebApplicationFactoryInMemDB<Startup> _factory;

    public LodgingTests(CustomWebApplicationFactoryInMemDB<Startup> factory)
    {
      _factory = factory;
    }

    [Theory]
    [InlineData()]
    public Task Post_LodgingControllerSuccessfullyCreatesDbEntry(string url)
    {
      //Arrange

      //Act

      //Assert

    }

    [Fact]
    public Task Get_SuccessfullyRetrievesAllLodgingFromDbEntries()
    {
      //Arrange

      //Act

      //Assert
    }

    [Theory]
    [InlineData()]
    public Task Get_RetrievesLodgingFromId(int id)
    {
      //Arrange

      //Act

      //Assert
    }

    [Theory]
    [InlineData()]
    public Task Delete_SuccessfullyRemovesLodgingFromDb(int id)
    {
      //Arrange

      //Act

      //Assert
    }
  }
}
