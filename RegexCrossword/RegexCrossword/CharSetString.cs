using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RegexCrossword.regex;

namespace RegexCrossword
{
  /// <summary>
  /// A (mutable) string where each character is a CharSet
  /// </summary>
  public class CharSetString : IEnumerable<CharSet>
  {
    private readonly IEnumerable<CharSet> _chars;

    public CharSetString(IEnumerable<CharSet> chars)
    {
      _chars = chars;
    }

    public int Length
    {
      get { return _chars.Count(); }
    }

    public static CharSetString UnconstrainedStringOfLength(int length)
    {
      var ret = new CharSet[length];
      for (int i = 0; i < length; i++)
      {
        ret[i] = new CharSet();
      }
      return new CharSetString(ret);
    }

    public static CharSetString EmptySetsStringOfLength(int length)
    {
      var ret = new CharSet[length];
      for (int i = 0; i < length; i++)
      {
        ret[i] = new CharSet {IsInclusive = true};
      }
      return new CharSetString(ret);
    }

    /// <summary>
    /// Epands the charsets in this string as necessary so that the given CharSetString
    /// is included in this one.
    /// </summary>
    /// <returns>this</returns>
    public CharSetString Union(CharSetString other)
    {
      foreach (var pair in _chars.ZipSameLength(other._chars))
      {
        pair.Item1.Union(pair.Item2);
      }
      return this;
    }

    /// <summary>
    /// Restricts the charsets in this string so that this string is no more inclusive
    /// than the given one.
    /// </summary>
    /// <returns>true if any changes were made</returns>
    public bool Intersect(CharSetString other)
    {
      var changesMade = false;
      foreach (var pair in _chars.ZipSameLength(other._chars))
      {
        changesMade |= pair.Item1.Intersect(pair.Item2);
      }
      return changesMade;
    }

    public CharSet this[int i]
    {
      get { return _chars.ElementAt(i); }
    }

    public IEnumerator<CharSet> GetEnumerator()
    {
      return _chars.GetEnumerator();
    }

    public override string ToString()
    {
      return string.Join("", _chars);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public static CharSetString Cons(CharSet first, CharSetString rest)
    {
      return new CharSetString(EnumerableExtensions.Cons(first, rest));
    }

    public CharSetString Concat(CharSetString rest)
    {
      return new CharSetString(_chars.Concat(rest._chars));
    }

    public static CharSetString EmptyString()
    {
      return new CharSetString(Enumerable.Empty<CharSet>());
    }

    /// <summary>
    /// Creates a CharSetString from a regex specification
    /// </summary>
    public static CharSetString Parse(string regexStyleFormat)
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
  }
}
