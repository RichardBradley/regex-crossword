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
