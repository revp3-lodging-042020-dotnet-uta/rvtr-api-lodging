using System;
using System.Collections.Generic;

namespace IntegrationTests
{
  public static class StaticTestingData
  {
    public static List<object[]> GetRequests=>
        new List<object[]>
        {
            new object[] { "api/v0.0/review" },
            new object[] { "api/v0.0/review/1" },
            new object[] { "api/v0.0/lodging" },
            new object[] { "api/v0.0/rental" },
        };
    }
  }
