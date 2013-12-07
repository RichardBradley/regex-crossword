using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexCrossword;

namespace RegexCrosswordTests
{
  [TestClass]
  public class RevertibleCharSetStringTest
  {
    [TestMethod]
    public void testReverting()
    {
      var rcs = RevertibleCharSetString.Parse("...");
      rcs.Bookmark();
      rcs[1].Intersect(CharSet.OneOf('A'));

      Assert.AreEqual(".A.", rcs.ToString());

      rcs.RevertToBookmark();

      Assert.AreEqual("...", rcs.ToString());
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void testDoubleBookmarkInvalid()
    {
      var rcs = RevertibleCharSetString.Parse("...");
      rcs.Bookmark();
      rcs.Bookmark();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void testRevertWithoutBookmarkInvalid()
    {
      var rcs = RevertibleCharSetString.Parse("...");
      rcs.RevertToBookmark();
    }
  }
}
