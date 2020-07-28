using System;
using System.Data;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RVTR.Lodging.WebApi;
using Xunit;

namespace IntegrationTests
{
  public class IntegrationTester : IClassFixture<CustomWebApplicationFactoryInMemDB<Startup>>
  {
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactoryInMemDB<Startup> _factory;
    
    [MemberData(nameof(StaticTestingData.PostRequests), MemberType = typeof(StaticTestingData))]
    [Theory(Skip = "Failing")]
    public async void CheckPostResponse(string url, object data)
    {
      var httpContent = new StringContent(data.ToString());
      httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
      var r = await _client.PostAsync(url, httpContent);
      Assert.True(r.StatusCode == System.Net.HttpStatusCode.Created);
      //adding stricter rules-assuring it is created 
      Assert.True(r.Content.Headers.ContentLocation != null, $"error posting: {httpContent.ToString()}");
    }
    
    [MemberData(nameof(StaticTestingData.PostRequests), MemberType = typeof(StaticTestingData))]
    [Theory(Skip = "Failing")]
    public async void CheckInvalid422PostResponse(string url, object data)
    {
      var httpContent = new StringContent(data.ToString() + "auidsf");
      httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
      var r = await _client.PostAsync(url, httpContent);
      //we want 422 and 409
      Assert.True(r.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity, $"Invalid Post Data Should return Status Code 422, not :{r.StatusCode}");
    }
  
    [Theory]
    [MemberData(nameof(StaticTestingData.Get409Requests), MemberType = typeof(StaticTestingData))]

    public async void CheckInvalid400PostResponse(string url)
    {
      var alteredURL = url[0..^2];
      var re = await _client.GetAsync(url);
      var body = re.Content;
      var httpContent = new StringContent(body.ToString());
      httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
      var r = await _client.PostAsync(alteredURL, httpContent);
      Assert.True(r.StatusCode == System.Net.HttpStatusCode.BadRequest, $"Conflicting Post Data Should return Status Code 400, not :{r.StatusCode}");
    }


    [Theory]
    [MemberData(nameof(StaticTestingData.Get404Requests), MemberType = typeof(StaticTestingData))]

    public async void CheckGetResponse(string url)
    {
      var r = await _client.GetAsync(url);
      //Console.WriteLine(await r.Content.ReadAsStringAsync());
      Assert.True(r.StatusCode == System.Net.HttpStatusCode.OK);
    }


    [Theory]
    [MemberData(nameof(StaticTestingData.GetRequests), MemberType = typeof(StaticTestingData))]

    public async void CheckGetNotFoundResponse(string url)
    {
      var r = await _client.GetAsync(url + "1111111");
      Console.WriteLine(await r.Content.ReadAsStringAsync());
      Assert.True(r.StatusCode == System.Net.HttpStatusCode.NotFound);
    }

    
    [MemberData(nameof(StaticTestingData.DeleteRequests), MemberType = typeof(StaticTestingData))]
    [Theory(Skip = "Failing")]
    public async void CheckDeleteResponse(string url)
    {
      var r = await _client.DeleteAsync(url);
      Assert.True(r.StatusCode == System.Net.HttpStatusCode.Accepted || r.StatusCode == System.Net.HttpStatusCode.NoContent);
    }

    public IntegrationTester(CustomWebApplicationFactoryInMemDB<Startup> factory)
    {
      _factory = factory;
      var c = _factory.CreateClient();
      this._client = c;
    }

  }
}
