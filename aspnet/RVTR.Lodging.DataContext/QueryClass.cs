using System;
using System.Collections.Generic;
using System.Text;

namespace RVTR.Lodging.DataContext
{
  public class QueryClass
  {
    public int Limit { get; set; }
    public int? PageStart { get; set;}
    public string Search { get; set; }
    public string SortKey { get; set; }
  }
}
