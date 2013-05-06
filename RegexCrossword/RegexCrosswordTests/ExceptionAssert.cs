using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegexCrosswordTests
{
  static class ExceptionAssert
  {
    public static void AssertThrows<T>(Action action)
    {
      try
      {
        action();
      }
      catch (Exception e)
      {
        Assert.IsInstanceOfType(e, typeof(T));
        return;
      }
      Assert.Fail("Expected exception");
    }
  }
}
