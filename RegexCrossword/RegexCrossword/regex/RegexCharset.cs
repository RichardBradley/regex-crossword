
using System.Collections.Generic;

namespace RegexCrossword.regex
{
  public class RegexCharset : RegexNonTerminalAtom
  {
    public CharSet CharSet;

    /// <summary>
    /// Creates a new "inclusive" charset.
    /// 
    /// If chars[] is empty, then no char will match
    /// </summary>
    /// <param name="chars"></param>
    public RegexCharset(params char[] chars)
    {
      CharSet = CharSet.OneOf(chars);
    }

    /// <summary>
    /// Adds a char.
    /// If the CharSet is inclusive, then this will be a new possible char;
    /// if it is exclusive, then this will be a new exclusion.
    /// </summary>
    public void AddChar(char ch)
    {
      CharSet.AddChar(ch);
    }

    public int Count
    {
      get { return CharSet.Count; }
    }

    public bool IsInclusive
    {
      get { return CharSet.IsInclusive; }
      set { CharSet.IsInclusive = value; }
    }

    public override bool Equals(object obj)
    {
      return obj.GetType() == GetType()
             && ((RegexCharset)obj).CharSet.Equals(CharSet);
    }

    public override int GetHashCode()
    {
      return 117;
    }

    /// <summary>
    /// Yields each of the possible matches of the regex from this point onwards as a CharSetString.
    /// </summary>
    /// <param name="charIdx">
    /// The current index of the match in the string
    /// </param>
    /// <param name="currentConstraints">
    /// The prior constraints on any matches
    /// </param>
    public override IEnumerable<CharSetString> GeneratePossibleMatches(int charIdx, CharSetString currentConstraints)
    {
      if (charIdx >= currentConstraints.Length)
      {
        yield break; // no match
      }
      var thisCharSet = currentConstraints[charIdx].Clone();
      try
      {
        thisCharSet.Intersect(CharSet);
      }
      catch (CharSet.EmptyIntersectionException)
      {
        yield break; // no match
      }

      foreach (var remainderMatch in Next.GeneratePossibleMatches(charIdx + 1, currentConstraints))
      {
        yield return CharSetString.Cons(thisCharSet, remainderMatch);
      }
    }

    public override string ToString()
    {
      return string.Format("{0}[{1}]",
        GetType(), CharSet);
    }
  }
}
