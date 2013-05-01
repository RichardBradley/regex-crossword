using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexCrossword;
using RegexCrossword.regex;

namespace RegexCrosswordTests.regex
{
  [TestClass]
  public class RegexTest
  {
    [TestMethod]
    public void TestOrConstraint()
    {
      var regex = new Regex("(ND|ET|IN)[^X]*");
      var str = CharSetString.UnconstrainedStringOfLength(7);
      Assert.IsTrue(regex.AddConstraints(str));

      Assert.AreEqual(CharSet.OneOf('N', 'E', 'I'), str[0]);
      Assert.AreEqual(CharSet.OneOf('D', 'T', 'N'), str[1]);
      Assert.AreEqual(CharSet.AnyExcept('X'), str[2]);
      Assert.AreEqual(CharSet.AnyExcept('X'), str[3]);
      Assert.AreEqual(CharSet.AnyExcept('X'), str[4]);
      Assert.AreEqual(CharSet.AnyExcept('X'), str[5]);
      Assert.AreEqual(CharSet.AnyExcept('X'), str[6]);
    }

    [TestMethod]
    public void TestParsing()
    {
      Assert.AreEqual(
        new Regex(
          new RegexZeroOrMore(new RegexAnyChar()),
          new RegexLiteralChar('H'),
          new RegexZeroOrMore(new RegexAnyChar()),
          new RegexLiteralChar('H'),
          new RegexZeroOrMore(new RegexAnyChar())),
        new Regex(".*H.*H.*"));

      Assert.AreEqual(
        new Regex(
          new RegexCapturingGroupChoices(
            RegexLiteralChar.ForString("ND"),
            RegexLiteralChar.ForString("ET"),
            RegexLiteralChar.ForString("IN")),
          new RegexZeroOrMore(
            new RegexCharset('X') { IsInclusive = false })),
        new Regex("(ND|ET|IN)[^X]*"));

      Assert.AreEqual(
        new Regex(
          new RegexLiteralChar('F'),
          new RegexZeroOrMore(new RegexAnyChar()),
          new RegexCharset('A', 'O'),
          new RegexZeroOrMore(new RegexAnyChar()),
          new RegexCharset('A', 'O'),
          new RegexZeroOrMore(new RegexAnyChar())),
        new Regex("F.*[AO].*[AO].*"));
    }
  }
}
