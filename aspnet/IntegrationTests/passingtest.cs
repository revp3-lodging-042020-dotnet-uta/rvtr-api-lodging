using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
namespace IntegrationTests
{
  [TestClass]
  public class passingtest
  {
    [TestMethod]
    public void passing()
    {
      Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(true);
    }
  }
}
