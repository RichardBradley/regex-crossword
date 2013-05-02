using System.Collections.Generic;

namespace RegexCrossword.regex
{
  public class RegexAnyChar : RegexNonTerminalAtom
  {
    public override bool Equals(object obj)
    {
      return obj.GetType() == GetType();
    }

    public override int GetHashCode()
    {
      return 97;
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
      // qq make it read-only?
      var thisCharSet = currentConstraints[charIdx];

      foreach (var remainderMatch in Next.GeneratePossibleMatches(charIdx + 1, currentConstraints))
      {
        yield return CharSetString.Cons(thisCharSet, remainderMatch);
      }
    }
  }
}
