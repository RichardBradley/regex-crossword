using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexCrossword;

namespace RegexCrosswordTests
{
  [TestClass]
  public class RevertibleCharSetStringTest
  {
    [TestMethod]
    public void TestReverting()
    {
      var rcs = RevertibleCharSetString.Parse("...");
      rcs.Bookmark();
      rcs[1].Intersect(CharSet.OneOf('A'));

      Assert.AreEqual(".A.", rcs.ToString());

      rcs.RevertToBookmark();

      Assert.AreEqual("...", rcs.ToString());
    }

    [TestMethod]
    public void TestRevertingTwice()
    {
      var rcs = RevertibleCharSetString.Parse("...");
      rcs.Bookmark();
      rcs.Intersect(CharSetString.Parse(".[AB]."));

      Assert.AreEqual(".[AB].", rcs.ToString());

      rcs.Bookmark();
      rcs.Intersect(CharSetString.Parse(".A."));

      Assert.AreEqual(".A.", rcs.ToString());

      rcs.RevertToBookmark();

      Assert.AreEqual(".[AB].", rcs.ToString());

      rcs.RevertToBookmark();

      Assert.AreEqual("...", rcs.ToString());
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestRevertWithoutBookmarkInvalid()
    {
      var rcs = RevertibleCharSetString.Parse("...");
      rcs.RevertToBookmark();
    }
  }
}
