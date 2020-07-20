using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationTests.Domains
{
public abstract class AbstractAPIControllerTester
{
    protected readonly HttpClient _client;
    public bool CheckPostResponse(HttpResponseMessage r)
    {
      throw new NotImplementedException();
    }
    public bool CheckGetResponse(HttpResponseMessage r)
    {
      return r.StatusCode == System.Net.HttpStatusCode.OK;
    }
    public bool CheckPutResponse(HttpResponseMessage r)
    {
      throw new NotImplementedException();
    }
    public bool CheckDeleteResponse(HttpResponseMessage r)
    {
      throw new NotImplementedException();
    }
    public AbstractAPIControllerTester(HttpClient c){
        this._client = c; 
    }

}
}