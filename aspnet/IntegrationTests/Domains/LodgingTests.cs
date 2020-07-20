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
    public void Post_LodgingControllerSuccessfullyCreatesDbEntry(string url)
    {
      //Arrange

      //Act

      //Assert

    }

    [Fact]
    public void Get_SuccessfullyRetrievesAllLodgingFromDbEntries()
    {
      //Arrange

      //Act

      //Assert
    }

    [Theory]
    [InlineData()]
    public void Get_RetrievesLodgingFromId(int id)
    {
      //Arrange

      //Act

      //Assert
    }

    [Theory]
    [InlineData()]
    public void Delete_SuccessfullyRemovesLodgingFromDb(int id)
    {
      //Arrange

      //Act

      //Assert
    }
  }
}
