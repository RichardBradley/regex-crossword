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
        ToCharSetString("...").Union(ToCharSetString("ABC")).ToString());

      // A + A = A
      // [^A] + A = .
      // [^B] + A = [^B]
      Assert.AreEqual(
        "A.[^B]",
        ToCharSetString("A[^A][^B]").Union(ToCharSetString("AAA")).ToString());

      // [AB] + A = [AB]
      // [AB] + C = [ABC]
      // [AB] + [BC] = [ABC]
      Assert.AreEqual(
        "[AB][ABC][ABC]",
        ToCharSetString("[AB][AB][AB]").Union(ToCharSetString("AC[BC]")).ToString());

      // [AB] + [^A] = .
      // [AB] + [^AB] = .
      // [AB] + [^ABC] = [^C]
      // [ABC] + [^CDE] = [^DE]
      Assert.AreEqual(
        "..[^C][^DE]",
        ToCharSetString("[AB][AB][AB][ABC]").Union(ToCharSetString("[^A][^AB][^ABC][^CDE]")).ToString());

      // [^A] + [^B] = .
      // [^AB] + [^BC] = [^B]
      Assert.AreEqual(
        ".[^B]",
        ToCharSetString("[^A][^AB]").Union(ToCharSetString("[^B][^BC]")).ToString());
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

      var charSetString = ToCharSetString(".[AB][AB][AB][^AB][^AB]");
      charSetString.Intersect(ToCharSetString("AA[BC][^AC][ABC][^BC]"));
      Assert.AreEqual(
        "AABBC[^ABC]",
        charSetString.ToString());

      // [AB] ^ [^AB] = error
      AssertThrows<CharSet.EmptyIntersectionException>(() => 
        ToCharSetString("[AB]").Intersect(ToCharSetString("[^AB]")));

      // [AB] ^ [CD] = error
      AssertThrows<CharSet.EmptyIntersectionException>(() =>
        ToCharSetString("[AB]").Intersect(ToCharSetString("[CD]")));
    }

    /// <summary>
    /// Creates a CharSetString from a regex specification
    /// </summary>
    private CharSetString ToCharSetString(string regexStyleFormat)
    {
      var regex = new Regex(regexStyleFormat);
      var charSets = new List<CharSet>();
      foreach (var atom in regex.Atoms)
      {
        if (atom is RegexAnyChar)
        {
          charSets.Add(new CharSet());
        }
        else if (atom is RegexCharset)
        {
          charSets.Add(((RegexCharset)atom).CharSet);
        }
        else if (atom is RegexLiteralChar)
        {
          charSets.Add(CharSet.OneOf(((RegexLiteralChar)atom).Ch));
        }
        else
        {
          throw new Exception("Unexpected type: " + atom);
        }
      }
      return new CharSetString(charSets.ToArray());
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
