using System;
using System.Net.Http;
using RVTR.Lodging.WebApi;
using Xunit;
using Xunit.Abstractions;
namespace IntegrationTests
{
  public class IntegrationTester : IClassFixture<CustomWebApplicationFactoryInMemDB<Startup>>
  {
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactoryInMemDB<Startup> _factory;

    [MemberData(nameof(StaticTestingData.PostRequests), MemberType = typeof(StaticTestingData))]
    [Theory]
    public async void CheckPostResponse(string url, object data)
    {
      var httpContent = new StringContent(data.ToString());
      httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

      //act
      var r = await _client.PostAsync(url, httpContent);
      //assert

      //Status Code Created
      Assert.Equal(System.Net.HttpStatusCode.Created, r.StatusCode);

      //LocationHeader Exists
      Assert.NotNull(r.Content.Headers.ContentLocation);

      //Make Get Request to LocationHeader URL
      var LocationExists = await _client.GetAsync(r.Content.Headers.ContentLocation);

      //LocationHeader Is Valid url
      Assert.Equal(System.Net.HttpStatusCode.OK, LocationExists.StatusCode);

    }
    
    [MemberData(nameof(StaticTestingData.PostRequests), MemberType = typeof(StaticTestingData))]
    [Theory]
    public async void CheckInvalid422PostResponse(string url, object data)
    {
      //arrange
      var httpContent = new StringContent(data.ToString() + Utils.GenerateString(6));
      httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

      //act
      var r = await _client.PostAsync(url, httpContent);

      //assert
          //response status code is equal to 422
      Assert.Equal(System.Net.HttpStatusCode.UnprocessableEntity, r.StatusCode);
    }

    [Theory]
    [MemberData(nameof(StaticTestingData.Get409Requests), MemberType = typeof(StaticTestingData))]
    public async void CheckInvalid409PostResponse(string GetUrl, string PostUrl)
    {
      //arange
          //adding a randomly generated char string to the url for an invalid post
      var re = await _client.GetAsync(GetUrl);
      var body = re.Content;
      var httpContent = new StringContent(body.ToString());
      httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

      //act
      var r = await _client.PostAsync(PostUrl, httpContent);

      //assert
          //response status code is 409
      Assert.Equal(System.Net.HttpStatusCode.Conflict, r.StatusCode);
    }


    [Theory]
    [MemberData(nameof(StaticTestingData.GetRequests), MemberType = typeof(StaticTestingData))]
    public async void CheckGetResponse(string url)
    {
      //act
      var r = await _client.GetAsync(url);

      //assert
          //response status code is OK
      Assert.Equal(System.Net.HttpStatusCode.OK, r.StatusCode);
          //response body is present/has the desired content type
      Assert.NotNull(r.Content);
      Assert.Equal("application/json; charset=utf-8", r.Content.Headers.ContentType.ToString()); 
    }


    [Theory]
    [MemberData(nameof(StaticTestingData.Get404Requests), MemberType = typeof(StaticTestingData))]
    public async void Check404Response(string url)
    {
      //act
      var r = await _client.GetAsync(url);
      Console.WriteLine(await r.Content.ReadAsStringAsync());
      //assert
        //response status code is 404
      Assert.Equal(System.Net.HttpStatusCode.NotFound, r.StatusCode);
      
    }

    
    [MemberData(nameof(StaticTestingData.DeleteRequests), MemberType = typeof(StaticTestingData))]
    [Theory]
    public async void CheckDeleteResponse(string url)
    {
      
      //act
      var r = await _client.DeleteAsync(url);

      //assert
          //response status code 202
      Assert.Equal(System.Net.HttpStatusCode.NoContent, r.StatusCode);
    }

    public IntegrationTester(CustomWebApplicationFactoryInMemDB<Startup> factory)
    {
      _factory = factory;
      var c = _factory.CreateClient();
      this._client = c;
    }

  }
}
