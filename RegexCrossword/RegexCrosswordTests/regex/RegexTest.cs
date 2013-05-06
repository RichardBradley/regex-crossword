using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexCrossword;
using RegexCrossword.regex;

namespace RegexCrosswordTests.regex
{
  [TestClass]
  public class RegexTest
  {
    [TestMethod]
    public void TestAddConstraints1()
    {
      var regex = new Regex("X*");
      var str = CharSetString.UnconstrainedStringOfLength(7);
      Assert.IsTrue(regex.AddConstraints(str));
      Assert.AreEqual("XXXXXXX", str.ToString());
    }

    [TestMethod]
    public void TestAddConstraints2()
    {
      var regex = new Regex("X*YX*");
      var str = CharSetString.UnconstrainedStringOfLength(7);
      Assert.IsTrue(regex.AddConstraints(str));
      Assert.AreEqual("[XY][XY][XY][XY][XY][XY][XY]", str.ToString());

      str = CharSetString.Parse("..Y....");
      Assert.IsTrue(regex.AddConstraints(str));
      Assert.AreEqual("XXYXXXX", str.ToString());

      str = CharSetString.Parse("..Y");
      Assert.IsTrue(regex.AddConstraints(str));
      Assert.AreEqual("XXY", str.ToString());
    }

    [TestMethod]
    public void TestAddConstraints3()
    {
      var regex = new Regex("(A|BC|DEF)(GHI|JK|L)");
      var str = CharSetString.UnconstrainedStringOfLength(4);
      Assert.IsTrue(regex.AddConstraints(str));
      Assert.AreEqual("[ABD][CEG][FHJ][IKL]", str.ToString());
    }

    [TestMethod]
    public void TestAddConstraints4()
    {
      var regex = new Regex("X+Y+Z+");
      var str = CharSetString.UnconstrainedStringOfLength(5);
      Assert.IsTrue(regex.AddConstraints(str));
      Assert.AreEqual("X[XY][XYZ][YZ]Z", str.ToString());

      str = CharSetString.Parse("...X..");
      Assert.IsTrue(regex.AddConstraints(str));
      Assert.AreEqual("XXXXYZ", str.ToString());

      str = CharSetString.Parse("..Z..");
      Assert.IsTrue(regex.AddConstraints(str));
      Assert.AreEqual("XYZZZ", str.ToString());
    }

    [TestMethod]
    public void TestOrConstraint()
    {
      var regex = new Regex("(ND|ET|IN)[^X]*");
      var str = CharSetString.UnconstrainedStringOfLength(7);
      Assert.IsTrue(regex.AddConstraints(str));

      Assert.AreEqual("[EIN][DNT][^X][^X][^X][^X][^X]", str.ToString());
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

      // This is an interesting one: we can see here that the match
      // must be: ABCDECBAE.
      // ... but our current implementation of backrefs cannot back-propagate
      // the constraints in the later use of the group to the earlier match
      // of the group.
      // To support this, we'd need to completely rework the match chain,
      // as it is currently based on constant prefixes modified by multiple varying
      // suffixes.
      str = CharSetString.Parse("ABCDE...ABC..");
      Assert.IsTrue(regex.AddConstraints(str));
      Assert.AreEqual("ABCDE...ABCE.", str.ToString());

      // Not enough info here to deduce anything
      str = CharSetString.Parse("..ABCD......");
      Assert.IsFalse(regex.AddConstraints(str));
      Assert.AreEqual("..ABCD......", str.ToString());
    }

    [TestMethod]
    public void TestParsing()
    {
      Assert.AreEqual(
        new Regex(
          RegexRepetitionModifier.ZeroOrMore(RegexCharset.AnyChar()),
          new RegexLiteralChar('H'),
          RegexRepetitionModifier.ZeroOrMore(RegexCharset.AnyChar()),
          new RegexLiteralChar('H'),
          RegexRepetitionModifier.ZeroOrMore(RegexCharset.AnyChar())),
        new Regex(".*H.*H.*"));

      Assert.AreEqual(
        new Regex(
          new RegexCapturingGroupChoices(
            RegexLiteralChar.ForString("ND"),
            RegexLiteralChar.ForString("ET"),
            RegexLiteralChar.ForString("IN")),
          new RegexRepetitionModifier(
            new RegexCharset('X') { IsInclusive = false }, 0, null)),
        new Regex("(ND|ET|IN)[^X]*"));

      Assert.AreEqual(
        new Regex(
          new RegexLiteralChar('F'),
          RegexRepetitionModifier.ZeroOrMore(RegexCharset.AnyChar()),
          new RegexCharset('A', 'O'),
          RegexRepetitionModifier.ZeroOrMore(RegexCharset.AnyChar()),
          new RegexCharset('A', 'O'),
          RegexRepetitionModifier.ZeroOrMore(RegexCharset.AnyChar())),
        new Regex("F.*[AO].*[AO].*"));
    }
  }
}
