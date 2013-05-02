using System.Collections.Generic;
using System.Linq;

namespace RegexCrossword.regex
{
  public class RegexLiteralChar : RegexNonTerminalAtom
  {
    public readonly char Ch;

    public RegexLiteralChar(char ch)
    {
      Ch = ch;
    }

    public static List<RegexAtom> ForString(string str)
    {
      return str.Select(ch => (RegexAtom)new RegexLiteralChar(ch)).ToList();
    }

    public override bool Equals(object obj)
    {
      return obj.GetType() == GetType()
        && Ch.Equals(((RegexLiteralChar)obj).Ch);
    }

    public override int GetHashCode()
    {
      return Ch.GetHashCode();
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
      if (charIdx >= currentConstraints.Length || !currentConstraints[charIdx].Contains(Ch))
      {
        yield break; // no matches
      }
      var thisCharSet = CharSet.OneOf(Ch);

      foreach (var remainderMatch in Next.GeneratePossibleMatches(charIdx + 1, currentConstraints))
      {
        yield return CharSetString.Cons(thisCharSet, remainderMatch);
      }
    }

    public override string ToString()
    {
      return string.Format("{0}[{1}]", GetType(), Ch);
    }
  }
}
