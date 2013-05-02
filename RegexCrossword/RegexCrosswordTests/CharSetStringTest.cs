using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexCrossword;
using RegexCrossword.regex;

namespace RegexCrosswordTests
{
  [TestClass]
  public class CharSetStringTest
  {
    [TestMethod]
    public void TestUnion()
    {
      // . doesn't need expanding:
      Assert.AreEqual(
        "...",
        CharSetString.Parse("...").Union(CharSetString.Parse("ABC")).ToString());

      // A + A = A
      // [^A] + A = .
      // [^B] + A = [^B]
      Assert.AreEqual(
        "A.[^B]",
        CharSetString.Parse("A[^A][^B]").Union(CharSetString.Parse("AAA")).ToString());

      // [AB] + A = [AB]
      // [AB] + C = [ABC]
      // [AB] + [BC] = [ABC]
      Assert.AreEqual(
        "[AB][ABC][ABC]",
        CharSetString.Parse("[AB][AB][AB]").Union(CharSetString.Parse("AC[BC]")).ToString());

      // [AB] + [^A] = .
      // [AB] + [^AB] = .
      // [AB] + [^ABC] = [^C]
      // [ABC] + [^CDE] = [^DE]
      Assert.AreEqual(
        "..[^C][^DE]",
        CharSetString.Parse("[AB][AB][AB][ABC]").Union(CharSetString.Parse("[^A][^AB][^ABC][^CDE]")).ToString());

      // [^A] + [^B] = .
      // [^AB] + [^BC] = [^B]
      Assert.AreEqual(
        ".[^B]",
        CharSetString.Parse("[^A][^AB]").Union(CharSetString.Parse("[^B][^BC]")).ToString());
    }

    [TestMethod]
    public void TestLengthChecks()
    {
      AssertThrows<EnumerableExtensions.EnumerationLengthsDifferException>(
        () => CharSetString.Parse("...").Union(CharSetString.Parse("..")));
      AssertThrows<EnumerableExtensions.EnumerationLengthsDifferException>(
        () => CharSetString.Parse("..").Union(CharSetString.Parse("...")));
      AssertThrows<EnumerableExtensions.EnumerationLengthsDifferException>(
        () => CharSetString.Parse("...").Intersect(CharSetString.Parse("..")));
      AssertThrows<EnumerableExtensions.EnumerationLengthsDifferException>(
        () => CharSetString.Parse("..").Intersect(CharSetString.Parse("...")));
    }

    [TestMethod]
    public void TestIntersect()
    {
      // . ^ [A] = [A]
      // [AB] ^ [A] = [A]
      // [AB] ^ [BC] = [B]
      // [AB] ^ [^AC] = [B]
      // [^AB] ^ [ABC] = [C]
      // [^AB] ^ [^BC] = [^ABC]

      var charSetString = CharSetString.Parse(".[AB][AB][AB][^AB][^AB]");
      charSetString.Intersect(CharSetString.Parse("AA[BC][^AC][ABC][^BC]"));
      Assert.AreEqual(
        "AABBC[^ABC]",
        charSetString.ToString());

      // [AB] ^ [^AB] = error
      AssertThrows<CharSet.EmptyIntersectionException>(() => 
        CharSetString.Parse("[AB]").Intersect(CharSetString.Parse("[^AB]")));

      // [AB] ^ [CD] = error
      AssertThrows<CharSet.EmptyIntersectionException>(() =>
        CharSetString.Parse("[AB]").Intersect(CharSetString.Parse("[CD]")));
    }

    private static void AssertThrows<T>(Action action)
    {
      try
      {
        action();
        Assert.Fail("Expected exception");
      }
      catch (Exception e)
      {
        Assert.IsInstanceOfType(e, typeof(T));
      }
    }
  }
}
