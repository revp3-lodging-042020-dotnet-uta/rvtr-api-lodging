using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationTests.Domains
{
public abstract class AbstractAPIControllerTester
{
    protected readonly HttpClient _client;
    public bool CheckPostResponse(ActionResult r)
    {
      throw new NotImplementedException();
    }
    public bool CheckGetResponse(ActionResult r)
    {
      throw new NotImplementedException();
    }
    public bool CheckPutResponse(ActionResult r)
    {
      throw new NotImplementedException();
    }
    public bool CheckDeleteResponse(ActionResult r)
    {
      throw new NotImplementedException();
    }
    public AbstractAPIControllerTester(HttpClient c){
        this._client = c; 
    }

}
}