using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using IntegrationTests.Utilities.Interfaces;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace IntegrationTests.Domains
{
  [CollectionDefinition("Client collection")]
  public class ReviewsTests : AbstractAPIControllerTester, IDelete, IGet, IPost, IPut
  {
    [Fact]
    public void PassingTest()
    {
      Assert.Equals(4, 4);
    }
    //Arrange
    [Theory]
    [InlineData()]
    public void CanPost(string url)
    {
      //Act

      //Assert

    }

    //Arrange
    [Theory]
    //[InlineData]
    public async void CanGet(string url)
    {
      //Act
      var r = await _client.GetAsync(url);
      //Assert
      Assert.IsTrue(CheckGetResponse(r));
    }

    //Arrange
    [Theory]
    [InlineData()]
    public void CanPut(int id)
    {


      //Act

      //Assert
    }

    //Arrange
    [Theory]
    [InlineData()]
    public void CanDelete(int id)
    {


      //Act

      //Assert
    }
    public ReviewsTests(HttpClient c) : base(c)
    {
    }
  }
}
