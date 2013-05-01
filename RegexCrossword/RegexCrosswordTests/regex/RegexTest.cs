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
      var str = CharSet.UnconstrainedStringOfLength(7);
      Assert.IsTrue(regex.AddConstraints(str));

      Assert.AreEqual(CharSet.OneOf('N', 'E', 'I'), str[0]);
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
            new RegexCharset('X') { IsInverse = true })),
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
