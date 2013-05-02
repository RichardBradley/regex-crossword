
using System.Collections.Generic;

namespace RegexCrossword.regex
{
  class RegexMaybe : RegexModifier
  {
    public RegexMaybe(RegexNonTerminalAtom inner)
      : base(inner)
    {
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
      // Matches with 'inner':
      foreach (var innerMatch in Inner.GeneratePossibleMatches(charIdx, currentConstraints))
      {
        yield return innerMatch;
      }

      // Matches without 'inner':
      foreach (var match in Next.GeneratePossibleMatches(charIdx, currentConstraints))
      {
        yield return match;
      }
    }
  }
}
