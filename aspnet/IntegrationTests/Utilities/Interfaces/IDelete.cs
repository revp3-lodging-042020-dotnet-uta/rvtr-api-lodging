using System;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationTests.Utilities.Interfaces
{
  public interface IDelete
  {
    public void CanDelete(int id);
  }
}
