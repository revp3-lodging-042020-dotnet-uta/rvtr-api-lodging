using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RVTR.Lodging.WebApi
{
  /// <summary>
  /// This class is used for capturing URI request parameters         var queryParams = new QueryModel(this.HttpContext.Request.Query);
  /// </summary>
  public class QueryModel
  {
    int? Limit { get; set; }
    int? Paginate { get; set; }
    string SortKey { get; set; }
    string SortOrder { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public QueryModel(IQueryCollection queryParams)
    {
      //-----------grab limit parameter from URI
      if (queryParams.ContainsKey("limit"))
      {
        int limit;
        if (Int32.TryParse(queryParams["limit"], out limit))
        {
          this.Limit = limit;
        }
        // defaults to null (int? Limit) vs 0 so that, DB will not return zero results ... else not required
      }

      //-----------grab paginate parameter from URI
      if (queryParams.ContainsKey("paginate"))
      {
          int paginate;
          if (Int32.TryParse(queryParams["paginate"], out paginate))
          {
            this.Paginate = paginate;
          }
       }

      //-----------grab sortkey parameter from URI
      if (queryParams.ContainsKey("sortkey"))
      {
        this.SortKey = queryParams["sortkey"];
      }

      //-----------grab sortOrder parameter from URI and sort by the value
      if (queryParams.ContainsKey("sortorder") )
      {
        this.SortOrder = queryParams["sortorder"] == "desc" ? "desc" : "asc";
      }
    }
  }
}
