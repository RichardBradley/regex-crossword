using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexCrossword;
using RegexCrossword.regex;

namespace RegexCrosswordTests.regex
{
  [TestClass]
  public class RegexBackReferenceTest
  {
    [TestMethod]
    public void TestLaterMatchesConstrainEarlierMatches()
    {
      var reg = new Regex("(.)\\1");
      var str = CharSetString.Parse(".A");
      reg.AddConstraints(str);

      Assert.AreEqual(
        @"AA",
        str.ToString());
    }

    [TestMethod]
    public void TestBackReferences1()
    {
      var regex = new Regex(@"(ABC)\1");
      var str = CharSetString.Parse("ABC...");
      Assert.IsTrue(regex.AddConstraints(str));
      Assert.AreEqual("ABCABC", str.ToString());

      str = CharSetString.Parse("ABCA..");
      Assert.IsTrue(regex.AddConstraints(str));
      Assert.AreEqual("ABCABC", str.ToString());

      str = CharSetString.Parse("ABCB..");
      ExceptionAssert.AssertThrows<CharSet.EmptyIntersectionException>(() =>
        regex.AddConstraints(str));
    }

    [TestMethod]
    public void TestBackReferences2()
    {
      var regex = new Regex(@"(...?)\1*");
      var str = CharSetString.Parse("ABC...");
      Assert.IsTrue(regex.AddConstraints(str));
      Assert.AreEqual("ABCABC", str.ToString());

      str = CharSetString.Parse("AB....");
      Assert.IsTrue(regex.AddConstraints(str));
      Assert.AreEqual("AB.[AB][AB].", str.ToString());

      str = CharSetString.Parse("ABA...");
      Assert.IsTrue(regex.AddConstraints(str));
      Assert.AreEqual("ABA[AB][AB][AB]", str.ToString());
    }

    [TestMethod]
    public void TestBackReferences3()
    {
      var regex = new Regex(@".*(.)(.)(.)(.)\4\3\2\1.*");
      var str = CharSetString.Parse("ABCD....");
      Assert.IsTrue(regex.AddConstraints(str));
      Assert.AreEqual("ABCDDCBA", str.ToString());

      str = CharSetString.Parse("....DCBA");
      Assert.IsTrue(regex.AddConstraints(str));
      Assert.AreEqual("ABCDDCBA", str.ToString());

      // This is an interesting one: with close inspection we can see here that the match
      // must be: ABCDECBAE.
      str = CharSetString.Parse("ABCDE...ABC..");
      Assert.IsTrue(regex.AddConstraints(str));
      Assert.AreEqual("ABCDECBAABCE.", str.ToString());

      // Not enough info here to deduce anything
      str = CharSetString.Parse("..ABCD......");
      Assert.IsFalse(regex.AddConstraints(str));
      Assert.AreEqual("..ABCD......", str.ToString());
    }
  }
}
